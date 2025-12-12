using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class ModulAYRINTIArama
    {
        public Int32? modulKimlik { get; set; }
        public string? modulAdi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? fotosu { get; set; }
        public bool? varmi { get; set; }
        public string? ikonAdi { get; set; }
        public Int32? sirasi { get; set; }
        public ModulAYRINTIArama()
        {
            varmi = true;
        }

        public async Task<List<ModulAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<ModulAYRINTI>(P => P.varmi == true);
            if (modulKimlik != null)
                predicate = predicate.And(x => x.modulKimlik == modulKimlik);
            if (modulAdi != null)
                predicate = predicate.And(x => x.modulAdi.Contains(modulAdi));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu.Contains(fotosu));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (ikonAdi != null)
                predicate = predicate.And(x => x.ikonAdi.Contains(ikonAdi));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            List<ModulAYRINTI> sonuc = new List<ModulAYRINTI>();
            sonuc = await vari.ModulAYRINTIs
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class ModulAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<ModulAYRINTI> ara(params Predicate<ModulAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.ModulAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.modulKimlik).ToList();
            }
        }


        public static List<ModulAYRINTI> tamami(Varlik kime)
        {
            return kime.ModulAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.modulKimlik).ToList();
        }
        public static async Task<ModulAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            ModulAYRINTI? kayit = await kime.ModulAYRINTIs.FirstOrDefaultAsync(p => p.modulKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static ModulAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            ModulAYRINTI kayit = kime.ModulAYRINTIs.FirstOrDefault(p => p.modulKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

