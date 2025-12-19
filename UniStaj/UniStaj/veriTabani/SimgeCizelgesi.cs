using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SimgeArama
    {
        public Int32? simgekimlik { get; set; }
        public string? baslik { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public bool? varmi { get; set; }
        public SimgeArama()
        {
            this.varmi = true;
        }

        public async Task<List<Simge>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<Simge>(P => P.varmi == true);
            if (simgekimlik != null)
                predicate = predicate.And(x => x.simgekimlik == simgekimlik);
            if (baslik != null)
                predicate = predicate.And(x => x.baslik != null && x.baslik.Contains(baslik));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<Simge> sonuc = new List<Simge>();
            sonuc = await vari.Simges
        .Where(predicate)
        .ToListAsync();
            return sonuc;
        }

    }


    public class SimgeCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<Simge>> ara(params Expression<Func<Simge, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<Simge>> ara(veri.Varlik vari, params Expression<Func<Simge, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.Simges
                            .Where(kosul).OrderByDescending(p => p.simgekimlik)
                   .ToListAsync();
        }



        public static async Task<Simge?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Simge? kayit = await kime.Simges.FirstOrDefaultAsync(p => p.simgekimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Simge yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.simgekimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Simges.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Simge? bulunan = await vari.Simges.FirstOrDefaultAsync(p => p.simgekimlik == yeni.simgekimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(Simge kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Simges.FirstOrDefaultAsync(p => p.simgekimlik == kimi.simgekimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }





    }
}


