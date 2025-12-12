using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class YetkiYokController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
