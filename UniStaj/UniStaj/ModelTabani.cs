using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniStaj.Models;
using UniStaj.veri;

namespace UniStaj
{
    public partial class ModelTabani
    {

        public string _fotoTanimi()
        {
            if (_ayar == null)
            {
                return "Fotoğraf";
            }
            else
            {
                int gen = _ayar.genislik ?? 0;
                int yuk = _ayar.yukseklik ?? 0;
                return String.Format("Foto {0}x{1} px", gen, yuk);
            }
        }
        ResimAyariAYRINTI _ayar { get; set; }

        public List<SelectListItem> doldur<T>(List<T> son) where T : Bilesen
        {
            List<SelectListItem> sonuc = new List<SelectListItem>();
            for (int i = 0; i < son.Count; i++)
                sonuc.Add(new SelectListItem(son[i]._tanimi(), son[i]._birincilAnahtar().ToString()));
            return sonuc;
        }
        public List<SelectListItem> doldur<T>(List<T> son, int[] secilenler) where T : Bilesen
        {
            List<SelectListItem> sonuc = new List<SelectListItem>();
            for (int i = 0; i < son.Count; i++)
                sonuc.Add(new SelectListItem(son[i]._tanimi(), son[i]._birincilAnahtar().ToString()));
            return sonuc;
        }
        protected async Task fotoAyariBelirle(veri.Varlik vari, string? cizelgeAdi = "Belirsiz")
        {
            _ayar = await vari.ResimAyariAYRINTIs.FirstOrDefaultAsync(p => p.ilgiliCizelge == cizelgeAdi);
        }
        public async Task fotoBicimlendirKos(veri.Varlik vari, Bilesen kartVerisi, long? fotoKimlik)
        {
            var uyan = await vari.ResimAyariAYRINTIs.FirstOrDefaultAsync(p => p.ilgiliCizelge == kartVerisi._cizelgeAdi());
            if (uyan == null)
            {
                ResimAyari ayar = new ResimAyari();
                ayar._varSayilan();
                ayar.ilgiliCizelge = kartVerisi._cizelgeAdi();
                ayar.genislik = 800;
                ayar.yukseklik = 600;
                ayar.kalite = 95;
                ayar.dizinAdi = kartVerisi._cizelgeAdi();
                await ayar.kaydetKos(vari, false);
                uyan = await vari.ResimAyariAYRINTIs.FirstOrDefaultAsync(p => p.ilgiliCizelge == kartVerisi._cizelgeAdi());
                ResimAyariModel modeli = new ResimAyariModel();
                modeli.yeniAyarKaydet(vari, ayar);
            }
            Fotograf? fotosu = await vari.Fotografs.FirstOrDefaultAsync(p => p.fotografKimlik == fotoKimlik);
            if (fotosu != null)
            {
                GenelIslemler.ResimIslemi.mansetKaydet(vari, fotosu, Genel.kayitKonumu, kartVerisi._cizelgeAdi(), kartVerisi._birincilAnahtar(), uyan);
            }
        }

        protected void fotoBicimlendir(veri.Varlik vari, Bilesen kartVerisi, long? fotoKimlik)
        {
            //var uyan = vari.ResimAyariAYRINTIs.FirstOrDefault(p => p.ilgiliCizelge == kartVerisi._cizelgeAdi());
            //if (uyan == null)
            //{
            //    ResimAyari ayar = new ResimAyari();
            //    ayar._varSayilan();
            //    ayar.ilgiliCizelge = kartVerisi._cizelgeAdi();
            //    ayar.genislik = 800;
            //    ayar.yukseklik = 600;
            //    ayar.kalite = 95;
            //    ayar.dizinAdi = kartVerisi._cizelgeAdi();
            //    ayar.kaydet(vari, false);
            //    uyan = vari.ResimAyariAYRINTIs.FirstOrDefault(p => p.ilgiliCizelge == kartVerisi._cizelgeAdi());
            //    ResimAyariModel modeli = new ResimAyariModel();
            //    modeli.yeniAyarKaydet(vari, ayar);
            //}
            //Fotograf fotosu = vari.Fotografs.FirstOrDefault(p => p.fotografKimlik == fotoKimlik);
            //if (fotosu != null)
            //{
            //    ResimIslemi.mansetKaydet(vari, fotosu, Genel._environmentWebRootPath, kartVerisi._cizelgeAdi(), kartVerisi._birincilAnahtar(), uyan);
            //}
        }

        public string fotoKonumu { get; set; }
        public int gecerliDil()
        {
            if (kullanan == null)
                return 1;
            else
                return kullanan.dilKimlik;
        }
        public Yonetici? kullanan;
        protected void hataFirlat(string ifade)
        {
            throw new System.Exception(ifade);
        }
        public bool yenimi { get; set; }
        public void yenimiBelirle(long? kimlik)
        {
            if (kimlik > 0)
                yenimi = false;
            else
                yenimi = true;
        }
        public SelectListItem eleman(object iki, object bir)
        {
            SelectListItem sonuc = new SelectListItem();
            sonuc.Text = iki.ToString();
            sonuc.Value = bir.ToString();
            return sonuc;
        }
        public List<SelectListItem> _evetHayir()
        {
            List<SelectListItem> sonuc = new List<SelectListItem>();
            sonuc.Add(eleman("Evet", true));
            sonuc.Add(eleman("Hayır", false));
            return sonuc;
        }
        public List<SelectListItem> _gonderimSekli()
        {
            List<SelectListItem> sonuc = new List<SelectListItem>();
            sonuc.Add(eleman("SMS", true));
            sonuc.Add(eleman("E-Posta", false));
            return sonuc;
        }

        /// <summary>
        ///parametre olarak girilen bütün kayıtları çeker.
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> _WebSayfasiAYRINTIler(List<WebSayfasiAYRINTI> hepsi)
        {
            List<SelectListItem> sonuc = new List<SelectListItem>();
            for (int i = 0; i < hepsi.Count; i++)
            {
                var yeni = eleman(hepsi[i]._tanimi(), hepsi[i]._birincilAnahtar());
                sonuc.Add(yeni);
            }
            return sonuc;
        }


        public List<SelectListItem> _RolAYRINTIler()
        {
            using (veri.Varlik vari = new Varlik())
            {
                List<SelectListItem> sonuc = new List<SelectListItem>();
                List<RolAYRINTI> hepsi = vari.RolAYRINTIs.ToList();
                for (int i = 0; i < hepsi.Count; i++)
                {
                    var yeni = eleman(hepsi[i]._tanimi(), hepsi[i]._birincilAnahtar());
                    sonuc.Add(yeni);
                }
                return sonuc;
            }
        }
        /// <summary>
        /// Rol çizelgesi içindeki bütün kayıtları çeker
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> _Roller()
        {
            using (veri.Varlik vari = new Varlik())
            {
                List<SelectListItem> sonuc = new List<SelectListItem>();
                List<RolAYRINTI> hepsi = vari.RolAYRINTIs.ToList();
                for (int i = 0; i < hepsi.Count; i++)
                {
                    var yeni = eleman(hepsi[i]._tanimi(), hepsi[i]._birincilAnahtar());
                    sonuc.Add(yeni);
                }
                return sonuc;
            }
        }
        /// <summary>
        /// KullaniciAYRINTI çizelgesi içindeki bütün kayıtları çeker
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> _KullaniciAYRINTIler()
        {
            using (veri.Varlik vari = new Varlik())
            {
                List<SelectListItem> sonuc = new List<SelectListItem>();
                List<KullaniciAYRINTI> hepsi = vari.KullaniciAYRINTIs.ToList();
                for (int i = 0; i < hepsi.Count; i++)
                {
                    var yeni = eleman(hepsi[i]._tanimi(), hepsi[i]._birincilAnahtar());
                    sonuc.Add(yeni);
                }
                return sonuc;
            }
        }
        /// <summary>
        ///parametre olarak girilen bütün kayıtları çeker.
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> _KullaniciAYRINTIler(List<KullaniciAYRINTI> hepsi)
        {
            List<SelectListItem> sonuc = new List<SelectListItem>();
            for (int i = 0; i < hepsi.Count; i++)
            {
                var yeni = eleman(hepsi[i]._tanimi(), hepsi[i]._birincilAnahtar());
                sonuc.Add(yeni);
            }
            return sonuc;
        }
        /// <summary>
        /// ref_KullaniciTuru çizelgesi içindeki bütün kayıtları çeker
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> _ref_KullaniciTuruler()
        {
            List<SelectListItem> sonuc = new List<SelectListItem>();
            List<ref_KullaniciTuru> hepsi = ref_KullaniciTuru.ara();
            for (int i = 0; i < hepsi.Count; i++)
            {
                var yeni = eleman(hepsi[i]._tanimi(), hepsi[i]._birincilAnahtar());
                sonuc.Add(yeni);
            }
            return sonuc;
        }

        /// <summary>
        /// ModulAYRINTI çizelgesi içindeki bütün kayıtları çeker
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> _ModulAYRINTIler()
        {
            List<SelectListItem> sonuc = new List<SelectListItem>();
            List<ModulAYRINTI> hepsi = ModulAYRINTI.ara();
            for (int i = 0; i < hepsi.Count; i++)
            {
                var yeni = eleman(hepsi[i]._tanimi(), hepsi[i]._birincilAnahtar());
                sonuc.Add(yeni);
            }
            return sonuc;
        }
        /// <summa
        /// <summary>
        /// ref_sayfaTuru çizelgesi içindeki bütün kayıtları çeker
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> _ref_sayfaTuruler()
        {
            List<SelectListItem> sonuc = new List<SelectListItem>();
            List<ref_sayfaTuru> hepsi = ref_sayfaTuru.ara();
            for (int i = 0; i < hepsi.Count; i++)
            {
                var yeni = eleman(hepsi[i]._tanimi(), hepsi[i]._birincilAnahtar());
                sonuc.Add(yeni);
            }
            return sonuc;
        }

    }
}
