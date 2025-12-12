using Microsoft.AspNetCore.Mvc;
using UniStaj.veri;


namespace UniStaj.Controllers
{
    public class RolWebSayfasiIzniController : Sayfa
    {
        [HttpPost]
        public IActionResult Goster(long deger)
        {
            Models.RolWebSayfasiIzniModel gelen = new Models.RolWebSayfasiIzniModel();
            gelen.olustur((int)deger);
            return PartialView("_RolIzniSayfasi", gelen);
        }

        public async Task<IActionResult> Index()
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "RolWebSayfasiIzni");
            gorunumAyari("", "", "Ana Sayfa", "/", "/RolWebSayfasiIzni/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.RolWebSayfasiIzniModel modeli = new Models.RolWebSayfasiIzniModel();
                modeli.veriCekKosut(mevcutKullanici());
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
        }
        public async Task<IActionResult> Kart(string id)
        {
            if (!oturumAcildimi())
                return OturumAcilmadi();

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "RolWebSayfasiIzni");
            gorunumAyari("Kartı", "Liste", "Ana Sayfa", "/", "/RolWebSayfasiIzni/", tanitim);
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await yetkiVarmiKos("RolWebSayfasiIzni", yetkiTuru))
            {

                Models.RolWebSayfasiIzniModel modeli = new Models.RolWebSayfasiIzniModel();
                await modeli.veriCekKos(mevcutKullanici(), id);
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Sil(string id)
        {
            try
            {
                if (id == null)
                    uyariVer("Hiç kayıt seçilmemiş");


                List<string> kayitlar = id.Split(',').ToList();
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos("RolWebSayfasiIzni", enumref_YetkiTuru.Silme))
                {
                    if (kayitlar.Count == 0)
                        uyariVer("Hiç kayıt seçilmemiş");

                    for (int i = 0; i < kayitlar.Count; i++)
                    {
                        RolWebSayfasiIzni silinecek = RolWebSayfasiIzni.olustur(kayitlar[i]);
                        silinecek.sil();
                    }
                    Models.RolWebSayfasiIzniModel modeli = new Models.RolWebSayfasiIzniModel();
                    modeli.veriCekKosut(mevcutKullanici());
                    return silindiBildirimi();
                }
                else
                {
                    return yetkiYokBildirimi();
                }
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }

        public class RolWebSayfasiIzniDto
        {
            public int rolWebSayfasiIzniKimlik { get; set; }
            public bool e_gormeIzniVarmi { get; set; }
            public bool e_eklemeIzniVarmi { get; set; }
            public bool e_silmeIzniVarmi { get; set; }
            public bool e_guncellemeIzniVarmi { get; set; }
        }


        public class RolWebSayfasiIzniGuncelleModel
        {
            public int kullaniciKimlik { get; set; }
            public List<RolWebSayfasiIzniDto> izinler { get; set; }
        }


        private int degeri(bool degisken)
        {
            if (degisken == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        [HttpPost]
        public async Task<ActionResult> Guncelle([FromBody] RolWebSayfasiIzniGuncelleModel model)
        {
            try
            {
                using (veri.Varlik vari = new Varlik())
                {
                    for (int i = 0; i < model.izinler.Count; i++)
                    {
                        var siradaki = model.izinler[i];
                        string sorgu = String.Format("  update rolWebSayfasiIzni set e_gormeIzniVarmi  = {0}, e_eklemeIzniVarmi = {1} ,  e_silmeIzniVarmi = {2}, e_guncellemeIzniVarmi = {3}  where rolWebSayfasiIzniKimlik = {4}", degeri(siradaki.e_gormeIzniVarmi), degeri(siradaki.e_eklemeIzniVarmi), degeri(siradaki.e_silmeIzniVarmi), degeri(siradaki.e_guncellemeIzniVarmi), siradaki.rolWebSayfasiIzniKimlik);
                        await SqlIslemi.islemYapKos(vari, sorgu);
                    }
                }

                return basariBildirimi("Başarıyla kaydedildi");
            }
            catch (Exception ex)
            {
                return hataBildirimi(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Kaydet(Models.RolWebSayfasiIzniModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                var kaydedilecek = gelen.kartVerisi;
                enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
                if (kaydedilecek._birincilAnahtar() > 0)
                    yetkiTuru = enumref_YetkiTuru.Guncelleme;
                if (await yetkiVarmiKos(kaydedilecek, yetkiTuru) == false)
                    throw new Exception("Bu işlemi yapmaya yetkiniz yok");

                using (veri.Varlik vari = new Varlik())
                {
                    kaydedilecek._kontrolEt(dilKimlik, vari);
                    kaydedilecek.kaydet();
                }
                return kaydedildiBildirimi(kaydedilecek);
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
    }
}
