using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniStaj.GenelIslemler;
using UniStaj.veri;

namespace UniStaj.Controllers
{
    public class GeciciDosyaController : Sayfa
    {

        public IActionResult Index()
        {
            return View();
        }

        private string uzantisi(string ad)
        {
            string[] adlar = ad.Split('.');
            return adlar[adlar.Length - 1];

        }
        [HttpPost]
        public ActionResult Yukle(IFormFile file)
        {
            string eskiAdi = "";
            string saveimg = "";
            string fileName = Guid.NewGuid().ToString();
            if (file != null)
            {

                eskiAdi = file.FileName;
                var Upload = Path.Combine(Genel.kayitKonumu, "GeciciDosyalar", fileName + "." + uzantisi(eskiAdi));
                FileStream akis = new FileStream(Upload, FileMode.Create);
                file.CopyTo(akis);
                akis.Close();
                saveimg = Upload;
            }
            string kon = "\\GeciciDosyalar\\" + fileName + "." + uzantisi(eskiAdi);
            return Json(new { location = kon });
        }


        [HttpPost]
        public ActionResult basvuruDosyasiYukle(IList<IFormFile> files, string id)
        {
            long kimlik = -100;
            try
            {

                IFormFile file = files[0];
                string eskiAdi = "";


                using (veri.Varlik vari = new veri.Varlik())
                {
                    string fileName = Guid.NewGuid().ToString();
                    if (file != null)
                    {
                        eskiAdi = file.FileName;
                        var Upload = Path.Combine(Genel.kayitKonumu, "GeciciDosyalar", fileName + "." + uzantisi(eskiAdi));
                        FileStream akis = new FileStream(Upload, FileMode.Create);
                        file.CopyTo(akis);
                        akis.Close();
                        akis.Dispose();
                        string kisa = "GeciciDosyalar\\" + fileName + "." + uzantisi(eskiAdi);

                        veri.YuklenenDosyalar dos = new YuklenenDosyalar();
                        dos.yuklemeTarihi = DateTime.Now;
                        dos.dosyaKonumu = kisa;
                        dos.ilgiliCizelgeAdi = "BursBasvuruDosyasi";
                        dos.ilgiliCizelgeKimlik = Convert.ToInt64(id);
                        dos.varmi = true;
                        dos.kodu = Guid.NewGuid().ToString();
                        dos.kaydet(vari, false);

                        return basariBildirimi(dos.yuklenenDosyalarKimlik.ToString());

                        //BursBasvuruDosyasi basvuru = vari.BursBasvuruDosyasis.FirstOrDefault(p => p.bursBasvuruDosyasiKimlik == Convert.ToInt64(id));
                        //basvuru.i_dosyaKimlik = dos.yuklenenDosyalarKimlik;
                        //basvuru.kodu = dos.kodu;
                        //basvuru.kaydet(vari, false);
                    }

                }
            }
            catch (Exception ex)
            {
                using (veri.Varlik vari = new Varlik())
                {
                    SistemHatasi yeni = new SistemHatasi();
                    yeni.aciklama = ex.ToString();
                    yeni.tarih = DateTime.Now;

                    vari.SistemHatasis.Add(yeni);
                    vari.SaveChanges();
                }
            }


            return basariBildirimi(kimlik.ToString());

        }

        [HttpPost]
        public async Task<ActionResult> GaleriyeYukle(IList<IFormFile> files, string kod)
        {
            try
            {
                using (veri.Varlik vari = new Varlik())
                {
                    GaleriAYRINTI? galerisi = await vari.GaleriAYRINTIs.FirstOrDefaultAsync(p => p.kodu == kod && p.varmi == true);
                    for (int i = 0; i < files.Count; i++)
                    {
                        List<GaleriFotosuAYRINTI> liste = await vari.GaleriFotosuAYRINTIs.Where(p => p.i_galeriKimlik == galerisi.galeriKimlik).ToListAsync();

                        int sira = liste.Count + 1;

                        IFormFile file = files[i];
                        string fileName = Guid.NewGuid().ToString();
                        if (file != null)
                        {
                            string eskiAdi = file.FileName;
                            var Upload = Path.Combine(Genel.kayitKonumu, "Galeri", fileName + "." + uzantisi(eskiAdi));
                            FileStream akis = new FileStream(Upload, FileMode.Create);
                            file.CopyTo(akis);
                            akis.Close();
                            akis.Dispose();
                            string kisa = "Galeri\\" + fileName + "." + uzantisi(eskiAdi);

                            veri.Fotograf dos = new Fotograf();
                            dos.yuklemeTarihi = DateTime.Now;
                            dos.konum = kisa;
                            dos.ilgiliKimlik = -1;
                            dos.varmi = true;
                            await vari.Fotografs.AddAsync(dos);
                            await vari.SaveChangesAsync();
                            ResimIslemi.boyutlandir(Upload, Upload, galerisi.genislik ?? 10, galerisi.yukseklik ?? 10, 90);
                            akis.Dispose();

                            GaleriFotosu yeni = new GaleriFotosu();
                            yeni.i_fotoKimlik = dos.fotografKimlik;
                            yeni.i_galeriKimlik = galerisi.galeriKimlik;
                            yeni.sirasi = sira;
                            await yeni.kaydetKos(vari, false);
                            sira++;
                        }

                    }
                }

                return basariBildirimi("Başarıyla yıklandı");
            }
            catch (Exception ex)
            {
                return hataBildirimi(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CokluYukle(IList<IFormFile> files)
        {
            string konum = "...";
            long kimlik = -100;
            try
            {
                IFormFile file = files[0];
                string eskiAdi = "";

                using (veri.Varlik vari = new veri.Varlik())
                {
                    string fileName = Guid.NewGuid().ToString();
                    if (file != null)
                    {
                        eskiAdi = file.FileName;
                        var Upload = Path.Combine(Genel.kayitKonumu, "GeciciDosyalar", fileName + "." + uzantisi(eskiAdi));
                        FileStream akis = new FileStream(Upload, FileMode.Create);
                        file.CopyTo(akis);
                        akis.Close();
                        akis.Dispose();
                        string kisa = "GeciciDosyalar\\" + fileName + "." + uzantisi(eskiAdi);

                        veri.Fotograf dos = new Fotograf();
                        dos.yuklemeTarihi = DateTime.Now;
                        dos.konum = kisa;
                        dos.ilgiliKimlik = -1;
                        dos.varmi = true;
                        await vari.Fotografs.AddAsync(dos);
                        await vari.SaveChangesAsync();
                        konum = dos.konum;
                        kimlik = dos.fotografKimlik;
                    }
                }
            }
            catch (Exception ex)
            {
                using (veri.Varlik vari = new Varlik())
                {
                    SistemHatasi yeni = new SistemHatasi();
                    yeni.aciklama = ex.ToString();
                    yeni.tarih = DateTime.Now;
                    vari.SistemHatasis.Add(yeni);
                    vari.SaveChanges();
                }
            }
            konum = konum.Replace("\\", "/");
            string sonuc = String.Format("{0}>/{1}", kimlik, konum);
            return basariBildirimi(sonuc);
        }


        [HttpPost]
        public async Task<ActionResult> CokluYukle2(IList<IFormFile> files, string yer, long fotoKimlik)
        {
            string konum = "...";
            long kimlik = -100;
            try
            {
                IFormFile file = files[0];
                string eskiAdi = "";

                using (veri.Varlik vari = new veri.Varlik())
                {
                    string fileName = Guid.NewGuid().ToString();
                    if (file != null)
                    {
                        eskiAdi = file.FileName;


                        Fotograf? dos = await vari.Fotografs.FirstOrDefaultAsync(p => p.fotografKimlik == fotoKimlik);

                        int genislik = 100;
                        int yukseklik = 100;

                        if (dos != null)
                        {
                            ResimAyariAYRINTI? ayar = await vari.ResimAyariAYRINTIs.FirstOrDefaultAsync(p => p.ilgiliCizelge == dos.ilgiliCizelge);
                            // Hakkimizda\c03b0281-5742-49fd-a5b1-165176e54071.webp



                            string ara = "";


                            //string temel = Path.Combine(Genel.kayitKonumu, kartVerisi.dizinAdi ?? "");
                            //string yeniOrta = Path.Combine(Genel.kayitKonumu, kartVerisi.dizinAdi + "\\Orta");
                            //string yeniKucuk = Path.Combine(Genel.kayitKonumu, kartVerisi.dizinAdi + "\\Kucuk");
                            //string yeniEnKucuk = Path.Combine(Genel.kayitKonumu, kartVerisi.dizinAdi + "\\EnKucuk");

                            if (yer == "1")
                                ara = "";
                            if (yer == "2")
                                ara = "Orta\\";
                            if (yer == "3")
                                ara = "Kucuk\\";
                            if (yer == "4")
                                ara = "EnKucuk\\";


                            if (ayar != null)
                            {
                                if (yer == "1")
                                {
                                    genislik = ayar.genislik ?? 100;
                                    yukseklik = ayar.yukseklik ?? 100;
                                }
                                if (yer == "2")
                                {
                                    genislik = ayar.genislik2 ?? 100;
                                    yukseklik = ayar.yukseklik2 ?? 100;
                                }
                                if (yer == "3")
                                {
                                    genislik = ayar.genislik3 ?? 100;
                                    yukseklik = ayar.yukseklik3 ?? 100;
                                }
                                if (yer == "4")
                                {
                                    genislik = ayar.genislik4 ?? 100;
                                    yukseklik = ayar.yukseklik4 ?? 100;
                                }

                            }

                            string ss = dos.konum.Replace(dos.ilgiliCizelge, "");
                            ss = ss.Replace("\\", "");

                            var Upload = Path.Combine(Genel.kayitKonumu, dos.ilgiliCizelge, ara, ss);
                            FileStream akis = new FileStream(Upload, FileMode.Create);
                            file.CopyTo(akis);
                            akis.Close();

                            ResimIslemi.boyutlandir(Upload, Upload, genislik, yukseklik, ayar.kalite ?? 75);
                            akis.Dispose();
                            konum = dos.konum ?? "";
                            kimlik = dos.fotografKimlik;
                            konum = Upload.Replace(Genel.kayitKonumu, "");
                            konum = Path.Combine(konum);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            string sonuc = String.Format("{0}>{1}", kimlik, konum);
            return basariBildirimi(sonuc);
        }



        [HttpPost]
        public ActionResult CokluDosyaYukle(IList<IFormFile> files)
        {

            IFormFile file = files[0];
            string eskiAdi = "";

            long kimlik = -100;

            veri.Varlik vari = new veri.Varlik();
            string fileName = Guid.NewGuid().ToString();
            if (file != null)
            {
                eskiAdi = file.FileName;
                var Upload = Path.Combine(Genel.kayitKonumu, "GeciciDosyalar", fileName + "." + uzantisi(eskiAdi));
                FileStream akis = new FileStream(Upload, FileMode.Create);
                file.CopyTo(akis);
                akis.Close();
                akis.Dispose();
                //saveimg = Upload;

                string kisa = "GeciciDosyalar\\" + fileName + "." + uzantisi(eskiAdi);



                veri.Fotograf dos = new Fotograf();
                dos.yuklemeTarihi = DateTime.Now;
                dos.konum = kisa;
                dos.ilgiliKimlik = -1;
                dos.varmi = true;
                vari.Fotografs.Add(dos);
                try
                {
                    vari.SaveChanges();
                }
                catch (Exception ex)
                {
                    var nedir = ex;
                }
                kimlik = dos.fotografKimlik;

            }



            return basariBildirimi(kimlik.ToString());

            //string kon = "\\GeciciDosyalar\\" + fileName + "." + uzantisi(eskiAdi);
            //return Json(new { location = kon });
        }


        [HttpPost]
        public async Task<ActionResult> SadeceDosyaYukle(IList<IFormFile> files)
        {

            IFormFile file = files[0];
            string eskiAdi = "";

            long kimlik = -100;

            veri.Varlik vari = new veri.Varlik();
            string fileName = Guid.NewGuid().ToString();
            if (file != null)
            {
                eskiAdi = file.FileName;
                var Upload = Path.Combine(Genel.kayitKonumu, "GeciciDosyalar", fileName + "." + uzantisi(eskiAdi));
                FileStream akis = new FileStream(Upload, FileMode.Create);
                file.CopyTo(akis);
                akis.Close();
                akis.Dispose();
                string kisa = "GeciciDosyalar\\" + fileName + "." + uzantisi(eskiAdi);
                veri.YuklenenDosyalar dos = new YuklenenDosyalar();
                dos.yuklemeTarihi = DateTime.Now;
                dos.dosyaKonumu = kisa;
                dos.ilgiliCizelgeKimlik = -1;
                dos.varmi = true;
                dos.kodu = Guid.NewGuid().ToString();
                vari.YuklenenDosyalars.Add(dos);
                try
                {
                    await vari.YuklenenDosyalars.AddAsync(dos);
                    await vari.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var nedir = ex;
                }
                kimlik = dos.yuklenenDosyalarKimlik;
            }

            return basariBildirimi(kimlik.ToString());
        }

        [HttpPost]
        public ActionResult DosyaYukle(IList<IFormFile> files)
        {

            IFormFile file = files[0];
            string eskiAdi = "";
            string saveimg = "";

            long kimlik = -100;

            veri.Varlik vari = new veri.Varlik();
            string fileName = Guid.NewGuid().ToString();
            if (file != null)
            {
                eskiAdi = file.FileName;
                var Upload = Path.Combine(Genel.kayitKonumu, "GeciciDosyalar", fileName + "." + uzantisi(eskiAdi));

                FileStream akis = new FileStream(Upload, FileMode.Create);
                file.CopyTo(akis);
                akis.Close();
                saveimg = Upload;
                akis.Dispose();
                string kisa = "GeciciDosyalar\\" + fileName + "." + uzantisi(eskiAdi);
                veri.YuklenenDosyalar dos = new YuklenenDosyalar();
                dos.yuklemeTarihi = DateTime.Now;
                dos.dosyaKonumu = kisa;
                dos.ilgiliCizelgeKimlik = -1;
                dos.varmi = true;
                vari.YuklenenDosyalars.Add(dos);
                try
                {
                    vari.SaveChanges();
                }
                catch (Exception ex)
                {
                    var nedir = ex;
                }
                kimlik = dos.yuklenenDosyalarKimlik;
            }
            return basariBildirimi(kimlik.ToString());
        }
        [HttpPost]
        public ActionResult CokluDosyaYukle2(IList<IFormFile> files)
        {

            string ifade = "";
            for (int i = 0; i < files.Count(); i++)
            {
                IFormFile file = files[i];
                string eskiAdi = "";
                string saveimg = "";

                long kimlik = -100;

                veri.Varlik vari = new veri.Varlik();
                string fileName = Guid.NewGuid().ToString();
                //string fileName = file.FileName;
                if (file != null)
                {
                    eskiAdi = file.FileName;
                    var Upload = Path.Combine(Genel.kayitKonumu, "GeciciDosyalar", fileName + "." + uzantisi(eskiAdi));
                    //var Upload = Path.Combine(Genel.kayitKonumu, "GeciciDosyalar", fileName );
                    file.CopyTo(new FileStream(Upload, FileMode.Create));
                    saveimg = Upload;

                    string kisa = "GeciciDosyalar/" + fileName + "." + uzantisi(eskiAdi);
                    veri.YuklenenDosyalar dos = new YuklenenDosyalar();
                    dos.yuklemeTarihi = DateTime.Now;
                    dos.dosyaKonumu = kisa;
                    dos.ilgiliCizelgeKimlik = -1;
                    dos.varmi = true;
                    vari.YuklenenDosyalars.Add(dos);
                    try
                    {
                        vari.SaveChanges();
                    }
                    catch
                    {
                    }
                    dos.dosyaKonumu = dos.dosyaKonumu;
                    kimlik = dos.yuklenenDosyalarKimlik;
                    if (i == 0)
                        ifade = dos.dosyaKonumu;
                    else
                        ifade = ifade + " <br/> " + dos.dosyaKonumu;
                }
            }
            return basariBildirimi(ifade);
        }
    }
}
