using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.Models
{
    public class AnaSayfaModel : ModelTabani
    {
        public string? fotosu { get; set; }
        public string? yazilimAdi { get; set; }
        public string? kisiAdi { get; set; }
        public string? kisiUnvani { get; set; }

        public string? baslik1 { get; set; }
        public string? baslik2 { get; set; }
        public string? baslik3 { get; set; }
        public string? baslik4 { get; set; }
        public string? baslik5 { get; set; }
        public string? baslik6 { get; set; }
        public string? baslik7 { get; set; }
        public string? baslik8 { get; set; }

        public string? deger1 { get; set; }
        public string? deger2 { get; set; }
        public string? deger3 { get; set; }
        public string? deger4 { get; set; }
        public string? deger5 { get; set; }
        public string? deger6 { get; set; }
        public string? deger7 { get; set; }
        public string? deger8 { get; set; }

        public SimgeAYRINTI simge1 { get; set; } = new SimgeAYRINTI();
        public SimgeAYRINTI simge2 { get; set; } = new SimgeAYRINTI();
        public SimgeAYRINTI simge3 { get; set; } = new SimgeAYRINTI();
        public SimgeAYRINTI simge4 { get; set; } = new SimgeAYRINTI();
        public SimgeAYRINTI simge5 { get; set; } = new SimgeAYRINTI();
        public SimgeAYRINTI simge6 { get; set; } = new SimgeAYRINTI();
        public SimgeAYRINTI simge7 { get; set; } = new SimgeAYRINTI();
        public SimgeAYRINTI simge8 { get; set; } = new SimgeAYRINTI();

        public int lisanssizSporcuSayisi { get; set; }
        public int topluYarismaBasvuruSayisi { get; set; }
        public int kulupDisiplinSayisi { get; set; }

        public bool BirimStajSorumlusuMu { get; set; }

        public void varsayilan()
        {
            duyurular = new List<DuyuruRolBagiAYRINTI>();
            baglantilar = new List<AnaSayfaBaglanti>();
            baslik1 = null;
            baslik2 = null;
            baslik3 = null;
            baslik4 = null;
            baslik5 = null;
            baslik6 = null;
            baslik7 = null;
            baslik8 = null;

            deger1 = null;
            deger2 = null;
            deger3 = null;
            deger4 = null;
            deger5 = null;
            deger6 = null;
            deger7 = null;
            deger8 = null;

            simge1.baslik = "";
            simge1.fotosu = "fa fa-building";

            simge2.baslik = "";
            simge2.fotosu = "fa fa-briefcase";

            simge3.baslik = "";
            simge3.fotosu = "fa fa-clock";

            simge4.baslik = "";
            simge4.fotosu = "fa fa-info-circle";

            baglantilar = new List<AnaSayfaBaglanti>();
        }

        public class AnaSayfaBaglanti
        {
            public int sirasi { get; set; }
            public string baslik { get; set; }
            public string baglanti { get; set; }
            public string aciklama { get; set; }

            public AnaSayfaBaglanti()
            {
                sirasi = 0;
                baslik = "";
                baglanti = "";
                aciklama = "";
            }
        }

        public List<AnaSayfaBaglanti> baglantilar { get; set; }
        public List<DuyuruRolBagiAYRINTI>? duyurular { get; set; }

        private SimgeAYRINTI simgeBul(List<SimgeAYRINTI> liste, string tanim)
        {
            SimgeAYRINTI? karsilik = liste.FirstOrDefault(p => p.baslik == tanim);
            if (karsilik != null)
                return karsilik;
            else
                return new SimgeAYRINTI();
        }

        public AnaSayfaModel()
        {
            varsayilan();
            baglantilar = new List<AnaSayfaBaglanti>();
        }
        private async Task stajSorumlusuEkrani(veri.Varlik vari, Yonetici kim)
        {

            StajBirimYetkilisiBirimi? birimYetkilisi = await vari.StajBirimYetkilisiBirimis.FirstOrDefaultAsync(x => x.i_stajBirimYetkilisiKimlik == kim.i_stajBirimYetkilisiKimlik);
            if (birimYetkilisi != null)
            {
                baslik1 = "Bağlı Olduğu Birim";
                var birim = await vari.StajBirimis.FirstOrDefaultAsync(b => b.stajBirimikimlik == birimYetkilisi.i_stajBirimiKimlik);

                if (birim != null)
                    deger1 = birim.stajBirimAdi;
                else
                    deger1 = "Birim Atanmamış";

                baslik2 = "Birime Başvuran  Öğrenci";
                var stajyerSayisi = await vari.Stajyers.CountAsync(x => x.i_stajBirimiKimlik == birimYetkilisi.i_stajBirimiKimlik);

                deger2 = stajyerSayisi + " Öğrenci";

                baslik3 = "Onay Bekleyen Başvuru Sayısı:";
                int onayBekleyenSayi = await vari.StajBasvurusuAYRINTIs.CountAsync(x => x.i_stajBasvuruDurumuKimlik == (int)enumref_StajBasvuruDurumu.Onay_Bekleniyor);

                deger3 = onayBekleyenSayi + " Başvuru";

            }

        }
        public async Task veriCekKosut(Yonetici kime)
        {
            using (veri.Varlik vari = new Varlik())
            {
                varsayilan();
                /*               
                List<SimgeAYRINTI> resimler = await vari.SimgeAYRINTIs.Where(p => p.varmi == true).ToListAsync();
                simge1 = simgeBul(resimler, "Araç");
                simge2 = simgeBul(resimler, "Görev");
                simge3 = simgeBul(resimler, "Personel");
                simge4 = simgeBul(resimler, "Talep");
                simge5 = simgeBul(resimler, "Araç");
                simge6 = simgeBul(resimler, "Araç");
                simge7 = simgeBul(resimler, "Araç");
                simge8 = simgeBul(resimler, "Araç");
                */


                duyurular = await vari.DuyuruRolBagiAYRINTIs.ToListAsync();
                try
                {
                    var liste = kime.rolleri.ToList();
                    for (int i = duyurular.Count - 1; i >= 0; i--)
                    {
                        int yer = liste.IndexOf(duyurular[i].i_rolKimlik);
                        if (yer == -1)
                            duyurular.RemoveAt(i);
                    }
                    duyurular = duyurular.OrderBy(p => p.sirasi).ToList();
                }
                catch
                {
                }
                duyurular = duyurular.Distinct().ToList();

                List<DuyuruRolBagiAYRINTI> tekiller = new List<DuyuruRolBagiAYRINTI>();
                foreach (var siradaki in duyurular)
                {
                    var karsilik = tekiller.FirstOrDefault(p => p.i_duyuruKimlik == siradaki.i_duyuruKimlik);
                    if (karsilik == null)
                        tekiller.Add(siradaki);
                }
                fotosu = kime.fotosu;
                kisiUnvani = kime.unvan;
                kisiAdi = kime.gercekAdi;
                yazilimAdi = Genel.yazilimAyari.yazilimAdi;
                var turu = kime._turu();

                this.BirimStajSorumlusuMu =
                    (turu == enumref_KullaniciTuru.Birim_Staj_Sorumlusu);

                if (turu == enumref_KullaniciTuru.Stajyer)
                {
                    Stajyer? stajyer = await vari.Stajyers.FirstOrDefaultAsync(x => x.stajyerkimlik == kime.i_stajyerKimlik);
                    await StajyerModel.stajyerinYukumlulukleriniOlustur(vari, stajyer);

                    if (stajyer != null)
                    {
                        baslik1 = "Bağlı Olduğu Birim";
                        var birim = await vari.StajBirimis.FirstOrDefaultAsync(b => b.stajBirimikimlik == stajyer.i_stajBirimiKimlik);

                        if (birim != null)
                            deger1 = birim.stajBirimAdi;
                        else
                            deger1 = "Birim Atanmamış";
                        var yukumlulukler = await vari.StajyerYukumluluks.Where(y => y.i_stajyerKimlik == stajyer.stajyerkimlik).ToListAsync();
                        baslik2 = "Zorunlu Staj Sayısı";
                        deger2 = yukumlulukler.Count + " Adet";

                        baslik3 = "Kalan Gün Sayısı";
                        int toplamGereken = yukumlulukler.Sum(y => y.gunSayisi ?? 0);
                        int toplamKabul = yukumlulukler.Sum(y => y.kabulEdilenGunSayisi ?? 0);
                        int kalan = toplamGereken - toplamKabul;
                        deger3 = (kalan > 0 ? kalan : 0) + " Gün";

                        baslik4 = "Son Başvuru Durumu";

                        var sonBasvuru = await vari.StajBasvurusus.Where(b => b.i_stajyerKimlik == stajyer.stajyerkimlik).OrderByDescending(b => b.baslangic).FirstOrDefaultAsync();

                        if (sonBasvuru != null)
                        {

                        }
                        else
                            deger4 = "Başvuru Yok";
                    }

                }


                if (turu == enumref_KullaniciTuru.Birim_Staj_Sorumlusu)
                {
                    await stajSorumlusuEkrani(vari, kime);
                }


            }
        }
    }
}
