using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class YetkiDegistirController : Sayfa
    {
        public async Task<IActionResult> yetkiDegistir(string id)
        {
            try
            {
                HttpContext.Session.Clear();
                await kullaniciYenile(Convert.ToInt32(id));
                return basariBildirimi("Başarıyla değiştirildi");
            }
            catch (Exception ex)
            {
                return hataBildirimi(ex);
            }
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                string tanitim = await Genel.dokumKisaAciklamaKos(this, "YetkiDegistir");
                gorunumAyari("", "", "Ana Sayfa", "/", "/YetkiDegistir/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                Models.YetkiDegistirModel modeli = new Models.YetkiDegistirModel();
                await modeli.yetkileriCek(mevcutKullanici());
                return View(modeli);
            }
            catch (Exception ex)
            {
                return await HataSayfasiKosut(ex);
            }
        }
    }
}
