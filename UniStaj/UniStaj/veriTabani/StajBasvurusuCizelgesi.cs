using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajBasvurusuArama
    {
        public Int32? stajBasvurusukimlik { get; set; }
        public Int32? i_stajyerKimlik { get; set; }
        public bool? varmi { get; set; }
        public string? stajKurumAdi { get; set; }
        public Int32? i_stajkurumturukimlik { get; set; }
        public string? hizmetAlani { get; set; }
        public string? vergiNo { get; set; }
        public string? ibanNo { get; set; }
        public Int32? calisanSayisi { get; set; }
        public string? adresi { get; set; }
        public string? telNo { get; set; }
        public string? ePosta { get; set; }
        public string? faks { get; set; }
        public string? webAdresi { get; set; }
        public string? yetkilisi { get; set; }
        public string? yetkiliGorevi { get; set; }
        public string? yetkiliTel { get; set; }
        public Int32? i_stajTuruKimlik { get; set; }
        public DateTime? baslangic { get; set; }
        public DateTime? bitis { get; set; }
        public Int32? gunSayisi { get; set; }
        public Int32? sinifi { get; set; }
        public string? calismaGunleri { get; set; }
        public string? kodu { get; set; }
        public StajBasvurusuArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajBasvurusu> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajBasvurusu>(P => P.varmi == true);
            if (stajBasvurusukimlik != null)
                predicate = predicate.And(x => x.stajBasvurusukimlik == stajBasvurusukimlik);
            if (i_stajyerKimlik != null)
                predicate = predicate.And(x => x.i_stajyerKimlik == i_stajyerKimlik);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (stajKurumAdi != null)
                predicate = predicate.And(x => x.stajKurumAdi != null && x.stajKurumAdi.Contains(stajKurumAdi));
            if (i_stajkurumturukimlik != null)
                predicate = predicate.And(x => x.i_stajkurumturukimlik == i_stajkurumturukimlik);
            if (hizmetAlani != null)
                predicate = predicate.And(x => x.hizmetAlani != null && x.hizmetAlani.Contains(hizmetAlani));
            if (vergiNo != null)
                predicate = predicate.And(x => x.vergiNo != null && x.vergiNo.Contains(vergiNo));
            if (ibanNo != null)
                predicate = predicate.And(x => x.ibanNo != null && x.ibanNo.Contains(ibanNo));
            if (calisanSayisi != null)
                predicate = predicate.And(x => x.calisanSayisi == calisanSayisi);
            if (adresi != null)
                predicate = predicate.And(x => x.adresi != null && x.adresi.Contains(adresi));
            if (telNo != null)
                predicate = predicate.And(x => x.telNo != null && x.telNo.Contains(telNo));
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (faks != null)
                predicate = predicate.And(x => x.faks != null && x.faks.Contains(faks));
            if (webAdresi != null)
                predicate = predicate.And(x => x.webAdresi != null && x.webAdresi.Contains(webAdresi));
            if (yetkilisi != null)
                predicate = predicate.And(x => x.yetkilisi != null && x.yetkilisi.Contains(yetkilisi));
            if (yetkiliGorevi != null)
                predicate = predicate.And(x => x.yetkiliGorevi != null && x.yetkiliGorevi.Contains(yetkiliGorevi));
            if (yetkiliTel != null)
                predicate = predicate.And(x => x.yetkiliTel != null && x.yetkiliTel.Contains(yetkiliTel));
            if (i_stajTuruKimlik != null)
                predicate = predicate.And(x => x.i_stajTuruKimlik == i_stajTuruKimlik);
            if (baslangic != null)
                predicate = predicate.And(x => x.baslangic == baslangic);
            if (bitis != null)
                predicate = predicate.And(x => x.bitis == bitis);
            if (gunSayisi != null)
                predicate = predicate.And(x => x.gunSayisi == gunSayisi);
            if (sinifi != null)
                predicate = predicate.And(x => x.sinifi == sinifi);
            if (calismaGunleri != null)
                predicate = predicate.And(x => x.calismaGunleri != null && x.calismaGunleri.Contains(calismaGunleri));
            if (kodu != null)
                predicate = predicate.And(x => x.kodu != null && x.kodu.Contains(kodu));
            return predicate;

        }
        public async Task<List<StajBasvurusu>> cek(veri.Varlik vari)
        {
            List<StajBasvurusu> sonuc = await vari.StajBasvurusus
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajBasvurusu?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajBasvurusu? sonuc = await vari.StajBasvurusus
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajBasvurusuCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajBasvurusu>> ara(params Expression<Func<StajBasvurusu, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajBasvurusu>> ara(veri.Varlik vari, params Expression<Func<StajBasvurusu, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajBasvurusus
                            .Where(kosul).OrderByDescending(p => p.stajBasvurusukimlik)
                   .ToListAsync();
        }



        public static async Task<StajBasvurusu?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajBasvurusu? kayit = await kime.StajBasvurusus.FirstOrDefaultAsync(p => p.stajBasvurusukimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(StajBasvurusu yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajBasvurusukimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.StajBasvurusus.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                StajBasvurusu? bulunan = await vari.StajBasvurusus.FirstOrDefaultAsync(p => p.stajBasvurusukimlik == yeni.stajBasvurusukimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(StajBasvurusu kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.StajBasvurusus.FirstOrDefaultAsync(p => p.stajBasvurusukimlik == kimi.stajBasvurusukimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static StajBasvurusu? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajBasvurusu? kayit = kime.StajBasvurusus.FirstOrDefault(p => p.stajBasvurusukimlik == kimlik);
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
        public static void kaydet(StajBasvurusu yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajBasvurusukimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.StajBasvurusus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.StajBasvurusus.FirstOrDefault(p => p.stajBasvurusukimlik == yeni.stajBasvurusukimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(StajBasvurusu kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.StajBasvurusus.FirstOrDefault(p => p.stajBasvurusukimlik == kimi.stajBasvurusukimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


