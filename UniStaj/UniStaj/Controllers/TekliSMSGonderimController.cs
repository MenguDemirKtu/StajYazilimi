using Microsoft.AspNetCore.Mvc;
using UniStaj.Models;

namespace UniStaj.Controllers
{
    public class TekliSMSGonderimController : Sayfa
    {
        public async Task<IActionResult> Index()
        {
            try
            {

                string tanitim = await Genel.dokumKisaAciklamaKos(this, "TekliSMSGonderimModel");
                gorunumAyari("", "", "Ana Sayfa", "/", "/TekliSMSGonderimModel/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos())
                {
                    TekliSMSGonderimModel modeli = new TekliSMSGonderimModel();
                    return View(modeli);
                }
                else
                {
                    return YetkiYok();
                }
            }
            catch (Exception ex)
            {
                return await HataSayfasiKosut(ex);
            }

        }

        public async Task<IActionResult> Gonder(TekliSMSGonderimModel gelen)
        {
            try
            {
                if (string.IsNullOrEmpty(gelen.metin))
                    throw new Exception("Metin girilmelidir");
                if (string.IsNullOrEmpty(gelen.telNolar))
                    throw new Exception("Telefon numaraları girilmelidir");
                int son = await gelen.gonderKos();
                return basariBildirimi(son.ToString() + " adet SMS başarıyla gönderildi");
            }
            catch (Exception ex)
            {
                return hataBildirimi(ex);
            }
        }
    }
}
