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

        public SimgeAYRINTI? simge1 { get; set; }
        public SimgeAYRINTI? simge2 { get; set; }
        public SimgeAYRINTI? simge3 { get; set; }
        public SimgeAYRINTI? simge4 { get; set; }
        public SimgeAYRINTI? simge5 { get; set; }
        public SimgeAYRINTI? simge6 { get; set; }
        public SimgeAYRINTI? simge7 { get; set; }
        public SimgeAYRINTI? simge8 { get; set; }

        public int lisanssizSporcuSayisi { get; set; }
        public int topluYarismaBasvuruSayisi { get; set; }
        public int kulupDisiplinSayisi { get; set; }
        public void varsayilan()
        {
            duyurular = new List<DuyuruRolBagiAYRINTI>();
            baglantilar = new List<AnaSayfaBaglanti>();
            baslik1 = "ASDASD";
            baslik2 = "FDSGD";
            baslik3 = "GDFGD";
            baslik4 = "DFGDF";
            baslik5 = "DFGDFGDFG";
            baslik6 = "DFG";
            baslik7 = "DFG";
            baslik8 = "DFG";

            deger1 = "1";
            deger2 = "2";
            deger3 = "3";
            deger4 = "4";
            deger5 = "5";
            deger6 = "6";
            deger7 = "7";
            deger8 = "8";
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

        //private async Task yazilimciBilgisiCek(veri.varlik vari, Yonetici kimi)
        //{
        //    baslik1 = "Değer 1";
        //    baslik2 = "Değer 2";
        //    baslik3 = "Değer 3";
        //    baslik4 = "Değer 4";
        //    baslik5 = "Değer 5";
        //    baslik6 = "Değer 6";
        //    baslik7 = "Değer 7";
        //    baslik8 = "Değer 8";

        //    deger1 = "";
        //    deger2 = "";
        //    deger3 = "";
        //    deger4 = "";
        //    deger5 = "";
        //    deger6 = "";
        //    deger7 = "";
        //    deger8 = "";


        //}
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

        public async Task veriCekKosut(Yonetici kime)
        {
            using (veri.Varlik vari = new Varlik())
            {
                varsayilan();

                List<SimgeAYRINTI> resimler = await vari.SimgeAYRINTIs.Where(p => p.varmi == true).ToListAsync();
                simge1 = simgeBul(resimler, "Araç");
                simge2 = simgeBul(resimler, "Görev");
                simge3 = simgeBul(resimler, "Personel");
                simge4 = simgeBul(resimler, "Talep");
                simge5 = simgeBul(resimler, "Araç");
                simge6 = simgeBul(resimler, "Araç");
                simge7 = simgeBul(resimler, "Araç");
                simge8 = simgeBul(resimler, "Araç");


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

                if (turu == enumref_KullaniciTuru.Stajyer)
                {
                    Stajyer? stajyer = await vari.Stajyers.FirstOrDefaultAsync(x => x.stajyerkimlik == kime.i_stajyerKimlik);
                    await StajyerModel.stajyerinYukumlulukleriniOlustur(vari, stajyer);

                }

                //if (turu == enumref_KullaniciTuru.Yazilimci)
                //{
                //    await yazilimciBilgisiCek(vari, kime);
                //}

                //bool iskurYetkilisimi = await kime.rolIslemiVarmi(vari, enumref_RolIslemi.İŞKUR_ÜNİVERSİTE_YETKİLİSİ);
                //if (iskurYetkilisimi)
                //    await iskurYetkiliCekKosut(vari, kime);

                //bool iskurGenelYetkilisimi = await kime.rolIslemiVarmi(vari, enumref_RolIslemi.ISKUR_BOLUM_YETKILISI);
                //if (iskurGenelYetkilisimi)
                //    await iskurBolumCekKosut(vari, kime);
            }
        }
    }
}
