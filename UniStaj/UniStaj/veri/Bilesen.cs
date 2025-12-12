using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public class uyariIfadesi
    {
        public string parca { get; set; }
        public object gosterimDegeri { get; set; }

        public uyariIfadesi()
        {
            parca = "";
            gosterimDegeri = "";
        }

        public uyariIfadesi(string _parca, object _deger)
        {
            if (_parca.IndexOf('{') == -1)
                _parca = "{" + _parca;

            if (_parca.IndexOf('}') == -1)
                _parca = _parca + "}";

            parca = _parca;
            gosterimDegeri = _deger;
        }
    }

    public class Bilesen
    {

        protected string bossaDoldur(string? ifade)
        {
            if (string.IsNullOrEmpty(ifade))
                return "";
            else
                return ifade;
        }
        protected string bossaDoldur(int? ifade)
        {
            if (ifade == null)
                return "";
            else
                return ifade.ToString();
        }
        protected string bossaDoldur(double? ifade)
        {
            if (ifade == null)
                return "";
            else
                return ifade.ToString();
        }

        protected string bossaDoldur(float? ifade)
        {
            if (ifade == null)
                return "";
            else
                return ifade.ToString();
        }

        protected string bossaDoldur(decimal? ifade)
        {
            if (ifade == null)
                return "";
            else
                return ifade.ToString();
        }

        protected string bossaDoldur(DateTime? ifade)
        {
            if (ifade == null)
                return "";
            else
                return ifade.ToString();
        }
        protected string bossaDoldur(bool? ifade)
        {
            if (ifade == null)
                return "";
            else
                return ifade.ToString();
        }
        private static string? GetPropertyValue(object obj, string propertyName)
        {
            var propInfo = obj.GetType().GetProperty(propertyName);
            if (propInfo == null)
                return null;
            return propInfo.GetValue(obj).ToString();
        }

        public string _fotoKonumu()
        {
            string? value = GetPropertyValue(this, "fotosu");
            if (value == null)
            {
                return "taf.png";
            }
            else
            {
                return value;
            }
        }

        public string _fotoEnKucuk()
        {
            string? value = GetPropertyValue(this, "fotosu");


            if (value == null)
            {
                return "taf.png";
            }
            else
            {
                string ad = this._cizelgeAdi();
                value = value.Replace(ad, ad + "\\EnKucuk\\");
                return value;
            }
        }
        public string _fotoKucuk()
        {
            string? value = GetPropertyValue(this, "fotosu");


            if (value == null)
            {
                return "taf.png";
            }
            else
            {
                string ad = this._cizelgeAdi();
                value = value.Replace(ad, ad + "\\Kucuk\\");
                return value;
            }
        }

        public string _fotoOrta()
        {
            string? value = GetPropertyValue(this, "fotosu");


            if (value == null)
            {
                return "taf.png";
            }
            else
            {
                string ad = this._cizelgeAdi();
                value = value.Replace(ad, ad + "\\Orta\\");
                return value;
            }
        }
        protected static List<SelectListItem> doldur2<T>(List<T> son) where T : Bilesen
        {
            List<SelectListItem> sonuc = new List<SelectListItem>();
            for (int i = 0; i < son.Count; i++)
                sonuc.Add(new SelectListItem(son[i]._tanimi(), son[i]._birincilAnahtar().ToString()));
            return sonuc;
        }
        protected static List<SelectListItem> doldur2<T>(List<T> son, int[] secililer) where T : Bilesen
        {
            List<int> liste = secililer.ToList();
            List<SelectListItem> sonuc = new List<SelectListItem>();
            for (int i = 0; i < son.Count; i++)
            {
                var yeni = new SelectListItem(son[i]._tanimi(), son[i]._birincilAnahtar().ToString());
                if (liste.IndexOf(Convert.ToInt32(son[i]._birincilAnahtar())) != -1)
                {
                    yeni.Selected = true;
                }
                sonuc.Add(yeni);
            }
            return sonuc;
        }

        public static List<SelectListItem> doldur<T>(List<T> son) where T : Bilesen
        {
            List<SelectListItem> sonuc = new List<SelectListItem>();
            for (int i = 0; i < son.Count; i++)
                sonuc.Add(new SelectListItem(son[i]._tanimi(), son[i]._birincilAnahtar().ToString()));
            return sonuc;
        }
        public static List<SelectListItem> doldur<T>(List<T> son, int[] secililer) where T : Bilesen
        {
            List<int> liste = secililer.ToList();
            List<SelectListItem> sonuc = new List<SelectListItem>();
            for (int i = 0; i < son.Count; i++)
            {
                var yeni = new SelectListItem(son[i]._tanimi(), son[i]._birincilAnahtar().ToString());
                if (liste.IndexOf(Convert.ToInt32(son[i]._birincilAnahtar())) != -1)
                {
                    yeni.Selected = true;
                }
                sonuc.Add(yeni);
            }
            return sonuc;
        }

        protected void sinirla(string ifade, int sinir)
        {
            if (string.IsNullOrEmpty(ifade))
                return;

            if (ifade.Length > sinir)
                ifade = ifade.Substring(0, sinir);
        }

        /// <summary>
        /// Bütün telefon numaralarını formatlar.  
        /// Söz gelimi + (0505)  515 2086 şeklindeki telefonu
        /// 5055152086 şekline getirir.
        /// </summary>
        /// <param name="ifade"></param>
        /// <returns></returns>
        private string butunTelefonNumaralari1(string ifade)
        {
            ifade = ifade.Replace('+', ' ');
            ifade = ifade.Replace(')', ' ');
            ifade = ifade.Replace('(', ' ');
            ifade = ifade.Trim();
            string[] pacalar = ifade.Split(' ');
            string tamami = "";
            for (int i = 0; i < pacalar.Length; i++)
                tamami += pacalar[i].Trim();


            long sonuc = 0;
            if (long.TryParse(tamami, out sonuc))
            {
                if (sonuc.ToString().Length == 10)
                    return sonuc.ToString();
                else
                    return ".";
            }
            else
                return ".";
        }

        protected long kayitKullaniciKimlik;



        /// <summary>
        /// İşlemi yapanın hangi kullanıcı olduğunu belirler.
        /// </summary>
        /// <returns></returns>
        public long _kullaniciDegeri()
        {
            return kayitKullaniciKimlik;
        }
        public void _kullaniciAta(Yonetici kimin)
        {
            kayitKullaniciKimlik = kimin.kullaniciKimlik;
        }
        /// <summary>
        /// Kayıt işlemleri için 
        /// Kullanıcnı kaydeden kullanıcı işlemibi belirler.
        /// </summary>
        /// <param name="_sayfasi"></param>
        public void _sayfaAta(Sayfa _sayfasi)
        {

            try
            {
                var kul = _sayfasi.mevcutKullanici();
                kayitKullaniciKimlik = kul.kullaniciKimlik;
            }
            catch
            {

            }
        }

        public void telefonNumarasiniKontrolEt(string ifade)
        {
            if (ifade.Trim() == "")
                return;


            string no = butunTelefonNumaralari1(ifade);
            if (no == ".")
                throw new Exception("Telefon numarası formata uygun değil. Numaralar  312 311 9120 biçiminde girilmelidir. ");
        }

        /// <summary>
        /// E-posta adrsinin formata uynuluğunu denetler. Eğer formata uygun değilse hata fırlatır.
        /// </summary>
        /// <param name="adres"></param>
        /// <returns></returns>
        public string ePostaAdresiFormatla(string adres)
        {
            string sonuc = adres.Trim();
            sonuc = sonuc.Replace(" ", "").Trim();
            if (sonuc == "")
                return sonuc;

            int yer = sonuc.IndexOf('@');
            if (yer == 0)
                throw new Exception("E-posta adresi @ ile başlayamaz");

            if (yer == -1)
                throw new Exception("E-Posta adresi @ karakteri içermelidir.");


            yer = sonuc.IndexOf('ı');
            if (yer != -1)
                throw new Exception("E-posta adresi ı,ç,ğ,ö,ş,ü gibi Türkçe karakterler içeremez");
            yer = sonuc.IndexOf('ç');
            if (yer != -1)
                throw new Exception("E-posta adresi ı,ç,ğ,ö,ş,ü gibi Türkçe karakterler içeremez");

            yer = sonuc.IndexOf('ğ');
            if (yer != -1)
                throw new Exception("E-posta adresi ı,ç,ğ,ö,ş,ü gibi Türkçe karakterler içeremez");

            yer = sonuc.IndexOf('ö');
            if (yer != -1)
                throw new Exception("E-posta adresi ı,ç,ğ,ö,ş,ü gibi Türkçe karakterler içeremez");

            yer = sonuc.IndexOf('ş');
            if (yer != -1)
                throw new Exception("E-posta adresi ı,ç,ğ,ö,ş,ü gibi Türkçe karakterler içeremez");

            yer = sonuc.IndexOf('ü');
            if (yer != -1)
                throw new Exception("E-posta adresi ı,ç,ğ,ö,ş,ü gibi Türkçe karakterler içeremez");
            return sonuc;
        }

        protected string cepTelFormatla(string numara)
        {
            if (string.IsNullOrEmpty(numara))
                return ".";

            numara = numara.Trim();

            if (numara.Length < 10)
                return ".";

            if (numara[0] == '0')
                numara = numara.Substring(1);


            if (numara.Length != 10)
                return ".";

            if (numara[0] != '5')
                return ".";


            return numara.Trim();

        }

        protected void uyariVerTarih(DateTime? tarih, string gorunurAdi, params int[] dil)
        {
            int dilKimlik = 1;
            if (dil.Length > 0)
            {
                dilKimlik = dil[0];
            }
            if (tarih == null || tarih == DateTime.MinValue)
                hataBildir(gorunurAdi, " seçilmelidir!", dilKimlik);
        }
        protected void uyariVerString(string deger, string gorunurAdi, params int[] dil)
        {
            int dilKimlik = 1;
            if (dil.Length > 0)
            {
                dilKimlik = dil[0];
            }

            if (string.IsNullOrEmpty(deger))
            {

                hataBildir(gorunurAdi, " doldurulmalıdır!", dilKimlik);
            }
        }
        protected void uyariVerByte(byte? deger, string gorunurAdi, int dilKimlik)
        {
            if (deger == null)
                hataBildir(gorunurAdi, " seçilmelidir!", dilKimlik);
        }
        protected void uyariVerBool(bool? deger, string gorunurAdi, int dilKimlik)
        {
            if (deger == null)
                hataBildir(gorunurAdi, " seçilmelidir!", dilKimlik);
        }
        protected void uyariVerInt(int? deger, string gorunurAdi, params int[] dil)
        {
            int dilKimlik = 1;
            if (dil.Length > 0)
            {
                dilKimlik = dil[0];
            }
            if (deger == 0 || deger == null)
                hataBildir(gorunurAdi, " seçilmelidir!", dilKimlik);
        }
        protected void uyariVerInt32(Int32? deger, string gorunurAdi, int dilKimlik)
        {
            if (deger == 0 || deger == null)
                hataBildir(gorunurAdi, " seçilmelidir!", dilKimlik);
        }
        protected void uyariVerInt64(Int64? deger, string gorunurAdi, int dilKimlik)
        {
            if (deger == 0 || deger == null)
                hataBildir(gorunurAdi, " seçilmelidir!", dilKimlik);
        }
        protected void uyariVerDecimal(decimal? deger, string gorunurAdi, int dilKimlik)
        {
            if (deger == 0 || deger == null)
                hataBildir(gorunurAdi, " seçilmelidir!", dilKimlik);
        }
        protected void uyariVerDouble(double? deger, string gorunurAdi, int dilKimlik)
        {
            if (deger == 0 || deger == null)
                hataBildir(gorunurAdi, " seçilmelidir!", dilKimlik);
        }
        protected void uyariVerLong(long? deger, string gorunurAdi, int dilKimlik)
        {
            if (deger == 0 || deger == null)
                hataBildir(gorunurAdi, " seçilmelidir!", dilKimlik);
        }

        private void hataBildir(string das, string iaf, int dilKimlik)
        {
            //   hataBildir(gorunurAdi, " seçilmelidir!", dilKimlik);  
            string ifade = Ayar.sozcuk(das, dilKimlik);
            string ara = Ikazlar.degeriSecilmelidir(dilKimlik);
            throw new Exception(string.Format(ara, ifade));
        }
        protected void uyariVer(Exception istisna)
        {
            throw new Exception(istisna.Message);
        }
        protected void uyariVer(string ifade, int dil)
        {
            ifade = Ayar.sozcuk(ifade, dil);
            throw new Exception(ifade);
        }
        public virtual void _varSayilan()
        {
        }

        //public virtual void _kontrolEt()
        //{
        //}
        public virtual void _kontrolEt(int dil, veri.Varlik vari)
        {

        }
        public virtual void _ayaraGoreKontrolEt(YazilimAyari ayar)
        {

        }
        public virtual string _birincilAnahtarAdi()
        {
            return "";
        }
        public virtual string _turkceAdi()
        {
            return "";
        }
        public virtual string _cizelgeAdi()
        {
            return "";
        }
        public virtual long _birincilAnahtar()
        {
            return 0;
        }
        public virtual string _tanimi()
        {
            return "tanım belirlenmemiş";
        }

        public virtual string _tanimi(int dil)
        {
            return "Tanım belirlenmemiş";
        }

        public virtual void _icDenetim(int dilKimlik, veri.Varlik vari)
        {

        }



    }
}