using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class TopluSmsGonderimController : Sayfa
    {
        public async Task<ActionResult> Duzenle(string id)
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "TopluSmsGonderimDuzenle");
            gorunumAyari("", "", "Toplu Gönderim Düzenle", "/", "/TopluSmsGonderimDuzenle/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.TopluSmsGonderimModel modeli = new Models.TopluSmsGonderimModel();
                await modeli.duzenleKos(id, mevcutKullanici());
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Gonder(Models.TopluSmsGonderimModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                await gelen.kaydetveGonderKos();
                return basariBildirimi(gelen.kartVerisi, dilKimlik);
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
        public async Task<IActionResult> Index()
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "TopluSmsGonderim");
            gorunumAyari("", "", "Ana Sayfa", "/", "/TopluSmsGonderim/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.TopluSmsGonderimModel modeli = new Models.TopluSmsGonderimModel();
                modeli.veriCekKosut(mevcutKullanici());
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

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "TopluSmsGonderim");
            gorunumAyari("Kartı", "Kartı", "Ana Sayfa", "/", "/TopluSmsGonderim/", tanitim);
            enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
            if (await yetkiVarmiKos("TopluSmsGonderim", yetkiTuru))
            {
                Models.TopluSmsGonderimModel modeli = new Models.TopluSmsGonderimModel();
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
                if (await yetkiVarmiKos("Ogrenci", enumref_YetkiTuru.Silme))
                {
                    Models.TopluSmsGonderimModel modeli = new Models.TopluSmsGonderimModel();
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
        public async Task<ActionResult> Kaydet(Models.TopluSmsGonderimModel gelen)
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
