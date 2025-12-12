using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class ResimAyariController : Sayfa
    {
        public ActionResult Cek(Models.ResimAyariModel modeli)
        {
            var nedir = modeli.ayrintiliAra(this);
            return basariBildirimi("/ResimAyari?id=" + nedir.kodu);
        }

        public async Task<ActionResult> Duzenle(string id)
        {
            try
            {
                string tanitim = "...";
                tanitim = await Genel.dokumKisaAciklamaKos(this, "ResimAyari");
                gorunumAyari("", "", "Ana Sayfa", "/", "/ResimAyari/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                Models.ResimAyariModel model = new Models.ResimAyariModel();
                await model.resimAyrintisiBelirle(mevcutKullanici(), Convert.ToInt64(id));

                return View(model);
            }
            catch (Exception ex)
            {
                return await HataSayfasiKosut(ex);
            }
        }
        public async Task<ActionResult> Index(string id)
        {
            string tanitim = "...";
            tanitim = await Genel.dokumKisaAciklamaKos(this, "ResimAyari");
            gorunumAyari("", "", "Ana Sayfa", "/", "/ResimAyari/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.ResimAyariModel modeli = new Models.ResimAyariModel();
                if (string.IsNullOrEmpty(id))
                    modeli.veriCekKosut(mevcutKullanici());
                else
                    modeli.kosulaGoreCek(mevcutKullanici(), id);
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
        }
        public async Task<ActionResult> Kart(long id)
        {
            if (!oturumAcildimi())
                return OturumAcilmadi();
            string tanitim = "....";
            tanitim = await Genel.dokumKisaAciklamaKos(this, "ResimAyari");
            gorunumAyari("Resim Ayarı Kartı", "Resim Ayarı Kartı", "Ana Sayfa", "/", "/ResimAyari/", tanitim);
            enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
            if (await yetkiVarmiKos("ResimAyari", yetkiTuru))
            {
                Models.ResimAyariModel modeli = new Models.ResimAyariModel();
                modeli.veriCek(mevcutKullanici(), id);
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
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (id == null)
                    uyariVer(Ikazlar.hicKayitSecilmemis(dilKimlik));
                if (await yetkiVarmiKos("ResimAyari", enumref_YetkiTuru.Silme))
                {
                    Models.ResimAyariModel modeli = new Models.ResimAyariModel();
                    modeli.sil(this, id, mevcutKullanici());
                    modeli.veriCekKosut(mevcutKullanici());
                    return basariBildirimi(Ikazlar.basariylaSilindi(dilKimlik));
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
        [HttpPost]
        public async Task<ActionResult> Kaydet(Models.ResimAyariModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                await gelen.yetkiKontrolu(this);
                gelen.kaydet(this);
                return basariBildirimi(gelen.kartVerisi, dilKimlik);
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
    }
}
