using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class KullaniciAYRINTIArama
    {
        public Int32? kullaniciKimlik { get; set; }
        public string? kullaniciAdi { get; set; }
        public string? sifre { get; set; }
        public string? guvenlikSorusu { get; set; }
        public string? guvenlikYaniti { get; set; }
        public Int32? i_kullaniciTuruKimlik { get; set; }
        public string? kullaniciTuru { get; set; }
        public string? gercekAdi { get; set; }
        public string? ePostaAdresi { get; set; }
        public string? telefon { get; set; }
        public string? unvan { get; set; }
        public bool? varmi { get; set; }
        public bool? e_rolTabanlimi { get; set; }
        public string? rolTabanlimi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? fotosu { get; set; }
        public string? tcKimlikNo { get; set; }
        public DateTime? dogumTarihi { get; set; }
        public Int32? i_dilKimlik { get; set; }
        public string? dilAdi { get; set; }
        public bool? e_faalmi { get; set; }
        public string? faalmi { get; set; }
        public string? ekAciklama { get; set; }
        public string? kodu { get; set; }
        public string? sicilNo { get; set; }
        public string? ogrenciNo { get; set; }
        public string? ustTur { get; set; }
        public string? fotoBilgisi { get; set; }
        public Int32? y_iskurOgrencisiKimlik { get; set; }
        public Int32? y_personelKimlik { get; set; }
        public Int64? y_kisiKimlik { get; set; }
        public DateTime? bysyeIlkGirisTarihi { get; set; }
        public bool? e_sifreDegisecekmi { get; set; }
        public DateTime? sonSifreDegistirmeTarihi { get; set; }
        public Int32? rolSayisi { get; set; }
        public bool? e_sozlesmeOnaylandimi { get; set; }
        public string? ciftOnayKodu { get; set; }
        public Int32? sonGunGirisi { get; set; }
        public int? y_stajBirimYetkilisiKimlik { get; set; }
        public KullaniciAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<KullaniciAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<KullaniciAYRINTI>(P => P.varmi == true);
            if (kullaniciKimlik != null)
                predicate = predicate.And(x => x.kullaniciKimlik == kullaniciKimlik);
            if (kullaniciAdi != null)
                predicate = predicate.And(x => x.kullaniciAdi != null && x.kullaniciAdi.Contains(kullaniciAdi));
            if (sifre != null)
                predicate = predicate.And(x => x.sifre != null && x.sifre.Contains(sifre));
            if (guvenlikSorusu != null)
                predicate = predicate.And(x => x.guvenlikSorusu != null && x.guvenlikSorusu.Contains(guvenlikSorusu));
            if (guvenlikYaniti != null)
                predicate = predicate.And(x => x.guvenlikYaniti != null && x.guvenlikYaniti.Contains(guvenlikYaniti));
            if (i_kullaniciTuruKimlik != null)
                predicate = predicate.And(x => x.i_kullaniciTuruKimlik == i_kullaniciTuruKimlik);
            if (kullaniciTuru != null)
                predicate = predicate.And(x => x.kullaniciTuru != null && x.kullaniciTuru.Contains(kullaniciTuru));
            if (gercekAdi != null)
                predicate = predicate.And(x => x.gercekAdi != null && x.gercekAdi.Contains(gercekAdi));
            if (ePostaAdresi != null)
                predicate = predicate.And(x => x.ePostaAdresi != null && x.ePostaAdresi.Contains(ePostaAdresi));
            if (telefon != null)
                predicate = predicate.And(x => x.telefon != null && x.telefon.Contains(telefon));
            if (unvan != null)
                predicate = predicate.And(x => x.unvan != null && x.unvan.Contains(unvan));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (e_rolTabanlimi != null)
                predicate = predicate.And(x => x.e_rolTabanlimi == e_rolTabanlimi);
            if (rolTabanlimi != null)
                predicate = predicate.And(x => x.rolTabanlimi != null && x.rolTabanlimi.Contains(rolTabanlimi));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu != null && x.fotosu.Contains(fotosu));
            if (tcKimlikNo != null)
                predicate = predicate.And(x => x.tcKimlikNo != null && x.tcKimlikNo.Contains(tcKimlikNo));
            if (dogumTarihi != null)
                predicate = predicate.And(x => x.dogumTarihi == dogumTarihi);
            if (i_dilKimlik != null)
                predicate = predicate.And(x => x.i_dilKimlik == i_dilKimlik);
            if (dilAdi != null)
                predicate = predicate.And(x => x.dilAdi != null && x.dilAdi.Contains(dilAdi));
            if (e_faalmi != null)
                predicate = predicate.And(x => x.e_faalmi == e_faalmi);
            if (faalmi != null)
                predicate = predicate.And(x => x.faalmi != null && x.faalmi.Contains(faalmi));
            if (ekAciklama != null)
                predicate = predicate.And(x => x.ekAciklama != null && x.ekAciklama.Contains(ekAciklama));
            if (kodu != null)
                predicate = predicate.And(x => x.kodu != null && x.kodu.Contains(kodu));
            if (sicilNo != null)
                predicate = predicate.And(x => x.sicilNo != null && x.sicilNo.Contains(sicilNo));
            if (ogrenciNo != null)
                predicate = predicate.And(x => x.ogrenciNo != null && x.ogrenciNo.Contains(ogrenciNo));
            if (ustTur != null)
                predicate = predicate.And(x => x.ustTur != null && x.ustTur.Contains(ustTur));
            if (fotoBilgisi != null)
                predicate = predicate.And(x => x.fotoBilgisi != null && x.fotoBilgisi.Contains(fotoBilgisi));
            if (y_iskurOgrencisiKimlik != null)
                predicate = predicate.And(x => x.y_iskurOgrencisiKimlik == y_iskurOgrencisiKimlik);
            if (y_personelKimlik != null)
                predicate = predicate.And(x => x.y_personelKimlik == y_personelKimlik);
            if (y_kisiKimlik != null)
                predicate = predicate.And(x => x.y_kisiKimlik == y_kisiKimlik);
            if (bysyeIlkGirisTarihi != null)
                predicate = predicate.And(x => x.bysyeIlkGirisTarihi == bysyeIlkGirisTarihi);
            if (e_sifreDegisecekmi != null)
                predicate = predicate.And(x => x.e_sifreDegisecekmi == e_sifreDegisecekmi);
            if (sonSifreDegistirmeTarihi != null)
                predicate = predicate.And(x => x.sonSifreDegistirmeTarihi == sonSifreDegistirmeTarihi);
            if (rolSayisi != null)
                predicate = predicate.And(x => x.rolSayisi == rolSayisi);
            if (e_sozlesmeOnaylandimi != null)
                predicate = predicate.And(x => x.e_sozlesmeOnaylandimi == e_sozlesmeOnaylandimi);
            if (ciftOnayKodu != null)
                predicate = predicate.And(x => x.ciftOnayKodu != null && x.ciftOnayKodu.Contains(ciftOnayKodu));
            if (sonGunGirisi != null)
                predicate = predicate.And(x => x.sonGunGirisi == sonGunGirisi);


            if (y_stajBirimYetkilisiKimlik != null)
                predicate = predicate.And(x => x.y_stajBirimYetkilisiKimlik == y_stajBirimYetkilisiKimlik);



            return predicate;

        }
        public async Task<List<KullaniciAYRINTI>> cek(veri.Varlik vari)
        {
            List<KullaniciAYRINTI> sonuc = await vari.KullaniciAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<KullaniciAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            KullaniciAYRINTI? sonuc = await vari.KullaniciAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class KullaniciAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<KullaniciAYRINTI>> ara(params Expression<Func<KullaniciAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<KullaniciAYRINTI>> ara(veri.Varlik vari, params Expression<Func<KullaniciAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.KullaniciAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.kullaniciKimlik)
                   .ToListAsync();
        }



        public static async Task<KullaniciAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            KullaniciAYRINTI? kayit = await kime.KullaniciAYRINTIs.FirstOrDefaultAsync(p => p.kullaniciKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static KullaniciAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            KullaniciAYRINTI? kayit = kime.KullaniciAYRINTIs.FirstOrDefault(p => p.kullaniciKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

