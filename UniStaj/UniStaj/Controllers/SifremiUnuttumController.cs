using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.GenelIslemler;
using UniStaj.Models;
using UniStaj.veri;

namespace UniStaj.Controllers
{
    public class SifremiUnuttumController : Sayfa
    {
        public async Task<ActionResult> Cek(Models.SifremiUnuttumModel modeli)
        {
            var nedir = await modeli.ayrintiliAraKos(this);
            return basariBildirimi("/SifremiUnuttum?id=" + nedir.kodu);
        }

        public ActionResult Animsat()
        {
            Models.SifremiUnuttumModel modeli = new Models.SifremiUnuttumModel();
            modeli.bos();
            return View(modeli);

        }

        public async Task<IActionResult> sifreGonder(SifremiUnuttumModel gelen)
        {
            try
            {
                Random rg = new Random();
                string sifre = rg.Next(100000, 900000).ToString();

                using (veri.Varlik vari = new Varlik())
                {
                    string telNo = Genel.telNoBicimlendir(gelen.kartVerisi.telefon ?? "");
                    string ePosta = Genel.epostaBicimlendir(gelen.kartVerisi.ePosta ?? "");

                    if (gelen.kartVerisi.e_smsmi == true)
                    {
                        if (telNo == ".")
                            throw new Exception("Lütfen telefon numaranızı 5xxxxxxx formatında giriniz.");

                        var kisiler = await vari.KullaniciAYRINTIs.Where(p => p.tcKimlikNo == gelen.kartVerisi.tcKimlikNo && p.telefon == telNo).ToListAsync();
                        if (kisiler.Count == 0)
                            throw new Exception("İlgili kullanıcı bulunamadı");


                        SifremiUnuttum unut = new SifremiUnuttum();
                        unut.tcKimlikNo = gelen.kartVerisi.tcKimlikNo;
                        unut.telefon = gelen.kartVerisi.telefon;
                        unut.ePosta = gelen.kartVerisi.ePosta;
                        unut.e_smsmi = true;
                        unut.tarih = DateTime.Now;
                        await unut.kaydetKos(vari, false);

                        Kullanici? kisi = await vari.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == kisiler[0].kullaniciKimlik);
                        if (kisi != null)
                        {
                            kisi.e_sifreDegisecekmi = true;
                            kisi.sifre = GenelIslemler.GuvenlikIslemi.sifrele(sifre);
                            await veriTabani.KullaniciCizelgesi.kaydetKos(kisi, vari, false);
                        }
                        KullaniciAYRINTI? insan = await vari.KullaniciAYRINTIs.FirstOrDefaultAsync(p => p.kullaniciKimlik == kisiler[0].kullaniciKimlik);
                        if (insan! == null)
                            await SMSIslemi.sifreAnimsatma(vari, insan, telNo);
                        return basariBildirimi("Şifreniz telefon numaranıza gönderildi");

                    }
                    else
                    {
                        if (ePosta == ".")
                            throw new Exception("Lütfen e-postanızı eposta@eposta.com formatında giriniz.");


                        var kisiler = await vari.KullaniciAYRINTIs.Where(p => p.tcKimlikNo == gelen.kartVerisi.tcKimlikNo && p.ePostaAdresi == ePosta).ToListAsync();
                        if (kisiler.Count == 0)
                            throw new Exception("İlgili kullanıcı bulunamadı");

                        SifremiUnuttum unut = new SifremiUnuttum();
                        unut.tcKimlikNo = gelen.kartVerisi.tcKimlikNo;
                        unut.telefon = gelen.kartVerisi.telefon;
                        unut.ePosta = gelen.kartVerisi.ePosta;
                        unut.e_smsmi = false;
                        unut.tarih = DateTime.Now;
                        await unut.kaydetKos(vari, false);


                        Kullanici? kisi = await vari.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == kisiler[0].kullaniciKimlik);
                        if (kisi != null)
                        {
                            kisi.e_sifreDegisecekmi = true;
                            kisi.sifre = sifre;
                            await veriTabani.KullaniciCizelgesi.kaydetKos(kisi, vari, false);
                            EPostaIslemi.SifreHatirlatma(vari, kisi);
                        }
                        return basariBildirimi("Şifreniz e-posta adresinize gönderildi");

                    }
                }
            }
            catch (Exception ex)
            {
                return hataBildirimi(ex);
            }

        }
        public async Task<ActionResult> Index(string id)
        {
            try
            {

                string tanitim = await Genel.dokumKisaAciklamaKos(this, "SifremiUnuttum");
                gorunumAyari("", "", "Ana Sayfa", "/", "/SifremiUnuttum/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos())
                {
                    Models.SifremiUnuttumModel modeli = new Models.SifremiUnuttumModel();
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
            catch (Exception ex)
            {
                return await HataSayfasiKosut(ex);
            }
        }
        public async Task<ActionResult> Kart(long id)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();

                string tanitim = await Genel.dokumKisaAciklamaKos(this, "SifremiUnuttum");
                gorunumAyari("Kartı", "Kartı", "Ana Sayfa", "/", "/SifremiUnuttum/", tanitim);
                enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
                if (await yetkiVarmiKos("SifremiUnuttum", yetkiTuru))
                {
                    Models.SifremiUnuttumModel modeli = new Models.SifremiUnuttumModel();
                    await modeli.veriCekKos(mevcutKullanici(), id);
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
                    Models.SifremiUnuttumModel modeli = new Models.SifremiUnuttumModel();
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
        public async Task<ActionResult> Kaydet(Models.SifremiUnuttumModel gelen)
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
