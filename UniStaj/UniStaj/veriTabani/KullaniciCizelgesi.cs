using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class KullaniciArama
    {
        public Int32? kullaniciKimlik { get; set; }
        public string? kullaniciAdi { get; set; }
        public string? sifre { get; set; }
        public string? guvenlikSorusu { get; set; }
        public string? guvenlikYaniti { get; set; }
        public Int32? i_kullaniciTuruKimlik { get; set; }
        public string? gercekAdi { get; set; }
        public string? ePostaAdresi { get; set; }
        public string? telefon { get; set; }
        public string? unvan { get; set; }
        public bool? varmi { get; set; }
        public bool? e_rolTabanlimi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? tcKimlikNo { get; set; }
        public DateTime? dogumTarihi { get; set; }
        public Int32? i_dilKimlik { get; set; }
        public bool? e_faalmi { get; set; }
        public string? ekAciklama { get; set; }
        public string? kodu { get; set; }
        public string? sicilNo { get; set; }
        public string? ogrenciNo { get; set; }
        public string? ustTur { get; set; }
        public string? fotoBilgisi { get; set; }
        public Int32? rolSayisi { get; set; }
        public Int64? y_kisiKimlik { get; set; }
        public Int32? y_iskurOgrencisiKimlik { get; set; }
        public Int32? y_personelKimlik { get; set; }
        public DateTime? bysyeIlkGirisTarihi { get; set; }
        public bool? e_sifreDegisecekmi { get; set; }
        public DateTime? sonSifreDegistirmeTarihi { get; set; }
        public KullaniciArama()
        {
            varmi = true;
        }

        public async Task<List<Kullanici>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<Kullanici>(P => P.varmi == true);
            if (kullaniciKimlik != null)
                predicate = predicate.And(x => x.kullaniciKimlik == kullaniciKimlik);
            if (kullaniciAdi != null)
                predicate = predicate.And(x => x.kullaniciAdi.Contains(kullaniciAdi));
            if (sifre != null)
                predicate = predicate.And(x => x.sifre.Contains(sifre));
            if (guvenlikSorusu != null)
                predicate = predicate.And(x => x.guvenlikSorusu.Contains(guvenlikSorusu));
            if (guvenlikYaniti != null)
                predicate = predicate.And(x => x.guvenlikYaniti.Contains(guvenlikYaniti));
            if (i_kullaniciTuruKimlik != null)
                predicate = predicate.And(x => x.i_kullaniciTuruKimlik == i_kullaniciTuruKimlik);
            if (gercekAdi != null)
                predicate = predicate.And(x => x.gercekAdi.Contains(gercekAdi));
            if (ePostaAdresi != null)
                predicate = predicate.And(x => x.ePostaAdresi.Contains(ePostaAdresi));
            if (telefon != null)
                predicate = predicate.And(x => x.telefon.Contains(telefon));
            if (unvan != null)
                predicate = predicate.And(x => x.unvan.Contains(unvan));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (e_rolTabanlimi != null)
                predicate = predicate.And(x => x.e_rolTabanlimi == e_rolTabanlimi);
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (tcKimlikNo != null)
                predicate = predicate.And(x => x.tcKimlikNo.Contains(tcKimlikNo));
            if (dogumTarihi != null)
                predicate = predicate.And(x => x.dogumTarihi == dogumTarihi);
            if (i_dilKimlik != null)
                predicate = predicate.And(x => x.i_dilKimlik == i_dilKimlik);
            if (e_faalmi != null)
                predicate = predicate.And(x => x.e_faalmi == e_faalmi);
            if (ekAciklama != null)
                predicate = predicate.And(x => x.ekAciklama.Contains(ekAciklama));
            if (kodu != null)
                predicate = predicate.And(x => x.kodu.Contains(kodu));
            if (sicilNo != null)
                predicate = predicate.And(x => x.sicilNo.Contains(sicilNo));
            if (ogrenciNo != null)
                predicate = predicate.And(x => x.ogrenciNo.Contains(ogrenciNo));
            if (ustTur != null)
                predicate = predicate.And(x => x.ustTur.Contains(ustTur));
            if (fotoBilgisi != null)
                predicate = predicate.And(x => x.fotoBilgisi.Contains(fotoBilgisi));
            if (rolSayisi != null)
                predicate = predicate.And(x => x.rolSayisi == rolSayisi);
            if (y_kisiKimlik != null)
                predicate = predicate.And(x => x.y_kisiKimlik == y_kisiKimlik);
            if (y_iskurOgrencisiKimlik != null)
                predicate = predicate.And(x => x.y_iskurOgrencisiKimlik == y_iskurOgrencisiKimlik);
            if (y_personelKimlik != null)
                predicate = predicate.And(x => x.y_personelKimlik == y_personelKimlik);
            if (bysyeIlkGirisTarihi != null)
                predicate = predicate.And(x => x.bysyeIlkGirisTarihi == bysyeIlkGirisTarihi);
            if (e_sifreDegisecekmi != null)
                predicate = predicate.And(x => x.e_sifreDegisecekmi == e_sifreDegisecekmi);
            if (sonSifreDegistirmeTarihi != null)
                predicate = predicate.And(x => x.sonSifreDegistirmeTarihi == sonSifreDegistirmeTarihi);
            List<Kullanici> sonuc = new List<Kullanici>();
            sonuc = await vari.Kullanicis
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class KullaniciCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<Kullanici> ara(params Predicate<Kullanici>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.Kullanicis.ToList().FindAll(kosul).OrderByDescending(p => p.kullaniciKimlik).ToList();
            }
        }


        public static List<Kullanici> tamami(Varlik kime)
        {
            return kime.Kullanicis.Where(p => p.varmi == true).OrderByDescending(p => p.kullaniciKimlik).ToList();
        }
        public static async Task<Kullanici?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Kullanici? kayit = await kime.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Kullanici yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            yeni.bicimlendir(vari);
            if (yeni.kullaniciKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Kullanicis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Kullanici? bulunan = await vari.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == yeni.kullaniciKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(Kullanici kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == kimi.kullaniciKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static Kullanici tekliCek(Int32 kimlik, Varlik kime)
        {
            Kullanici kayit = kime.Kullanicis.FirstOrDefault(p => p.kullaniciKimlik == kimlik);
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
        public static void kaydet(Kullanici yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.kullaniciKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Kullanicis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Kullanicis.FirstOrDefault(p => p.kullaniciKimlik == yeni.kullaniciKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Kullanici kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Kullanicis.FirstOrDefault(p => p.kullaniciKimlik == kimi.kullaniciKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


