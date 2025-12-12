using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
      using System;
    using System.Threading; 
   using Microsoft.EntityFrameworkCore; 
 using System.Threading;
 using System.Collections.Generic; 
  using System.Threading.Tasks;
using System.Linq;
using UniStaj.veri;
namespace UniStaj.Controllers
{
 public class PersonelController : Sayfa
    {
public async Task<ActionResult> Cek( Models.PersonelModel modeli)
                            {
                                var nedir = await modeli.ayrintiliAraKos(this);
                                return basariBildirimi("/Personel?id=" + nedir.kodu);
                            }
    public async Task<ActionResult> Index(string id)
        {
try{
            string tanitim = "...";
   tanitim = await Genel.dokumKisaAciklamaKos(this, "Personel"); 
 gorunumAyari("", "", "Ana Sayfa", "/", "/Personel/", tanitim); 
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
   Models.PersonelModel modeli = new Models.PersonelModel();
                if (string.IsNullOrEmpty(id))
  	await	   modeli.veriCekKos(mevcutKullanici());
	 else
    	await	 modeli.kosulaGoreCek(mevcutKullanici(), id);
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
}
catch(Exception ex)
{
return await HataSayfasiKosut(ex);
}
        }
public async Task<ActionResult> Kart(long id)
        {
try{
            if (!oturumAcildimi())
                return OturumAcilmadi();
            string tanitim = "....";
   tanitim = await Genel.dokumKisaAciklamaKos(this, "Personel"); 
 gorunumAyari("Kartı", "Kartı", "Ana Sayfa", "/", "/Personel/", tanitim); 
            enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
if (await yetkiVarmiKos("Personel", yetkiTuru))
            {
  Models.PersonelModel modeli = new Models.PersonelModel(); 
                  await   modeli.veriCekKos(mevcutKullanici(), id);
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
 }
 catch (Exception ex )
 {
     return await HataSayfasiKosut(ex);
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
Models.PersonelModel modeli = new Models.PersonelModel();
              await       modeli.silKos(this, id??"", mevcutKullanici());
                 await       modeli.veriCekKos(mevcutKullanici()); 
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
   public async Task<ActionResult> Kaydet(Models.PersonelModel gelen)
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
