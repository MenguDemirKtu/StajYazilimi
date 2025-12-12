using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class YenileController : Sayfa
    {
        public async Task<IActionResult> Index()
        {
            Models.YenileModel model = new Models.YenileModel();
            await model.yenileKos();
            return RedirectToAction("Index", "AnaSayfa");
        }
    }
}
