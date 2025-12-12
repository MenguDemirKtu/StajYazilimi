using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class CikisController : Sayfa
    {
        public IActionResult Index()
        {
            try
            {
                HttpContext.Session.Clear();
                Response.Redirect("/Giris", false);
            }
            catch
            {

            }
            return View();
        }
    }
}
