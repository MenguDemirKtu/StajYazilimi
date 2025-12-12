using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniStaj.Models;
using UniStaj.veri;

namespace UniStaj.Controllers
{
    public class SozlesmeController : Sayfa
    {
        public async Task<ActionResult> Cek(Models.SozlesmeModel modeli)
        {
            var nedir = await modeli.ayrintiliAraKos(this);
            return basariBildirimi("/Sozlesme?id=" + nedir.kodu);
        }

        public async Task<ActionResult> Onayla(string kod)
        {
            if (oturumAcildimi() == false)
                return OturumAcilmadi();
            SozlesmeModel modeli = new SozlesmeModel();
            await modeli.uyelikOnayiCekKos(mevcutKullanici(), kod);
            return View(modeli);
        }


        public async Task<ActionResult> giris(SozlesmeModel gelen)
        {
            try
            {
                SozlesmeOnayi onay = await gelen.onaylaKos();
                Yonetici onayi = mevcutKullanici();
                onayi.e_sozlesmeOnaylandimi = true;
                string jsonser = JsonConvert.SerializeObject(onayi);
                HttpContext.Session.SetString("mevcutKullanici", jsonser);
                return basariBildirimi(onay, 1, "/anasayfa");
            }
            catch (Exception ex)
            {
                return hataBildirimi(ex);
            }
        }


        public async Task<ActionResult> Index(string id)
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "Sozlesme");
            gorunumAyari("", "", "Ana Sayfa", "/", "/Sozlesme/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.SozlesmeModel modeli = new Models.SozlesmeModel();
                if (string.IsNullOrEmpty(id))
                    await modeli.veriCekKos(mevcutKullanici());
                else
                    await modeli.kosulaGoreCek(mevcutKullanici(), id);
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

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "Sozlesme");
            gorunumAyari("Sözleşme Kartı", "Sözleşme Kartı", "Ana Sayfa", "/", "/Sozlesme/", tanitim);
            enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
            if (await yetkiVarmiKos("Sozlesme", yetkiTuru))
            {
                Models.SozlesmeModel modeli = new Models.SozlesmeModel();
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
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (id == null)
                    uyariVer(Ikazlar.hicKayitSecilmemis(dilKimlik));
                if (await yetkiVarmiKos("Ogrenci", enumref_YetkiTuru.Silme))
                {
                    Models.SozlesmeModel modeli = new Models.SozlesmeModel();
                    await modeli.silKos(this, id, mevcutKullanici());
                    await modeli.veriCekKos(mevcutKullanici());
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
        public async Task<ActionResult> Kaydet(Models.SozlesmeModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                await gelen.yetkiKontrolu(this);
                await gelen.kaydetKos(this);
                return basariBildirimi(gelen.kartVerisi, dilKimlik);
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
    }
}
