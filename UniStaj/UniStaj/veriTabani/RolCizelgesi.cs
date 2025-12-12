using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class RolArama
    {
        public Int32? rolKimlik { get; set; }
        public string? rolAdi { get; set; }
        public string? tanitim { get; set; }
        public bool? e_gecerlimi { get; set; }
        public bool? varmi { get; set; }
        public bool? e_varsayilanmi { get; set; }
        public Int32? i_varsayilanOlduguKullaniciTuruKimlik { get; set; }
        public bool? e_rolIslemiIcinmi { get; set; }
        public Int32? i_rolIslemiKimlik { get; set; }
        public string? kodu { get; set; }
        public RolArama()
        {
            varmi = true;
        }

        public async Task<List<Rol>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<Rol>(P => P.varmi == true);
            if (rolKimlik != null)
                predicate = predicate.And(x => x.rolKimlik == rolKimlik);
            if (rolAdi != null)
                predicate = predicate.And(x => x.rolAdi != null && x.rolAdi.Contains(rolAdi));
            if (tanitim != null)
                predicate = predicate.And(x => x.tanitim != null && x.tanitim.Contains(tanitim));
            if (e_gecerlimi != null)
                predicate = predicate.And(x => x.e_gecerlimi == e_gecerlimi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (e_varsayilanmi != null)
                predicate = predicate.And(x => x.e_varsayilanmi == e_varsayilanmi);
            if (i_varsayilanOlduguKullaniciTuruKimlik != null)
                predicate = predicate.And(x => x.i_varsayilanOlduguKullaniciTuruKimlik == i_varsayilanOlduguKullaniciTuruKimlik);
            if (e_rolIslemiIcinmi != null)
                predicate = predicate.And(x => x.e_rolIslemiIcinmi == e_rolIslemiIcinmi);
            if (i_rolIslemiKimlik != null)
                predicate = predicate.And(x => x.i_rolIslemiKimlik == i_rolIslemiKimlik);
            if (kodu != null)
                predicate = predicate.And(x => x.kodu != null && x.kodu.Contains(kodu));
            List<Rol> sonuc = new List<Rol>();
            sonuc = await vari.Rols
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class RolCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<Rol> ara(params Predicate<Rol>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.Rols.ToList().FindAll(kosul).OrderByDescending(p => p.rolKimlik).ToList();
            }
        }


        public static List<Rol> tamami(Varlik kime)
        {
            return kime.Rols.Where(p => p.varmi == true).OrderByDescending(p => p.rolKimlik).ToList();
        }
        public static async Task<Rol?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Rol? kayit = await kime.Rols.FirstOrDefaultAsync(p => p.rolKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Rol yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.rolKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                await vari.Rols.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Rol? bulunan = await vari.Rols.FirstOrDefaultAsync(p => p.rolKimlik == yeni.rolKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(Rol kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Rols.FirstOrDefaultAsync(p => p.rolKimlik == kimi.rolKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static Rol? tekliCek(Int32 kimlik, Varlik kime)
        {
            Rol? kayit = kime.Rols.FirstOrDefault(p => p.rolKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }


        /// <summary>
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır.
        /// </summary>
        /// <param name="yeni"></param>
        /// <param name="kime"></param>
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param>
        public static void kaydet(Rol yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.rolKimlik <= 0)
            {
                kime.Rols.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Rols.FirstOrDefault(p => p.rolKimlik == yeni.rolKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Rol kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Rols.FirstOrDefault(p => p.rolKimlik == kimi.rolKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


