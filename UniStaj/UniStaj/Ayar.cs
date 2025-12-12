namespace UniStaj
{
    public static class Ayar
    {
        public static string tabloId = "datatable-buttons";
        public static string tabloClass = "table table-striped  dt-responsive nowrap";
        public static string tabloStyle = "border-collapse: separate; border-spacing: 0 5px; width: 100%;";
        /// <summary>
        /// Parametre oplarak girilen dile göre ifade oluşturur.
        /// </summary>
        /// <param name="ifade"></param>
        /// <param name="dilKimlik"></param>
        /// <returns></returns>
        public static string sozcuk(string ifade, int dilKimlik)
        {
            try
            {

                return ifade;

                ////if (dilKimlik == 1)
                ////    return ifade;
                ////else
                ////    return "EN : " + ifade;
                //var kayitli = Genel.sozcukler.FirstOrDefault(p => p.sozcuk == ifade && p.i_dilKimlik == dilKimlik);

                //if (kayitli != null)
                //    return kayitli.ifade;


                //if (string.IsNullOrEmpty(ifade))
                //{
                //    ifade = "";
                //    return ifade;
                //}

                //using (veri.Varlik  vari = new varlik())
                //{
                //    veri.BYSSozcukAYRINTI sozcugu = vari.BYSSozcukAYRINTIs.FirstOrDefault(p => p.sozcuk == ifade);
                //    if (sozcugu == null)
                //    {
                //        BYSSozcuk yeni = new BYSSozcuk();
                //        yeni.sozcuk = ifade;
                //        yeni.varmi = true;
                //        vari.BYSSozcuks.Add(yeni);
                //        vari.SaveChanges();

                //        BYSSozcukAciklama alt = new BYSSozcukAciklama();
                //        alt._varSayilan();
                //        alt.i_bysSozcukKimlik = Convert.ToInt32(yeni.bysSozcukKimlik);
                //        alt.i_dilKimlik = 2;
                //        alt.ifade = "EN : " + ifade;
                //        alt.varmi = true;

                //        vari.BYSSozcukAciklamas.Add(alt);
                //        vari.SaveChanges();

                //        return alt.ifade;
                //    }
                //    else
                //    {
                //        var uyan = vari.BYSSozcukAciklamaAYRINTIs.FirstOrDefault(p => p.i_dilKimlik == dilKimlik && p.i_bysSozcukKimlik == sozcugu.bysSozcukKimlik);
                //        if (uyan != null)
                //        {
                //            return uyan.ifade;
                //        }
                //        else
                //        {
                //            BYSSozcukAciklama alt = new BYSSozcukAciklama();
                //            alt._varSayilan();
                //            alt.i_bysSozcukKimlik = Convert.ToInt32(sozcugu.bysSozcukKimlik);
                //            alt.i_dilKimlik = 2;
                //            alt.ifade = "EN : " + ifade;
                //            alt.varmi = true;

                //            vari.BYSSozcukAciklamas.Add(alt);
                //            vari.SaveChanges();
                //            Genel.sozcukler = vari.BYSSozcukAciklamaAYRINTIs.ToList();
                //            return alt.ifade;
                //        }

                //    }
                //}

            }
            catch
            {
                return ifade;
            }
        }

        /// <summary>
        /// Geriye 31 Aralık 2023 gibi bir değer döndürür.
        /// </summary>
        /// <param name="tarih"></param>
        /// <returns></returns>
        public static string tarihAyAciklamasi(DateTime? tarih)
        {
            DateTime tar = tarih.Value;

            int gun = tar.Day;
            int ay = tar.Month;
            int yil = tar.Year;



            string[] adlar = new string[] {"Ocak",
          "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"};

            string ifade = String.Format("{0} {1} {2}", gun, adlar[ay - 1], yil);
            return ifade;

        }

        public static string yazi(string ifade)
        {
            return ifade;
        }

        public static string evetHayir(bool? ifade)
        {
            if (ifade == true)
                return "Evet";
            else
                return "Hayır";
        }
        /// <summary>
        /// Tarih boş değilse gün ay yıl türünden katara çevirir.
        /// </summary>
        /// <param name="tarih"></param>
        /// <returns></returns>
        public static string tarihGAY(DateTime? tarih)
        {
            if (tarih == null)
                return "";

            DateTime kesin = (DateTime)tarih;
            return kesin.ToString("d");
        }

        public static string aramaKutusuArkaPlan = "#fcf0db";

        public static string ucteBirGenislik = "col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12";
        public static string yariGenislik = "col-xl-6 col-lg-6 col-md-12 col-sm-12 col-12";
        public static string tamGenislik = "col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12";
        public static string formGrup = "form-group";
        public static string labelBicim = "col-form-label";
        public static string labelBicimKalin = "col-form-label kalin";
        public static string ceyrekGenislik = "col-xl-3 col-lg-3 col-md-12 col-sm-12 col-12";


        public static string aramaSutunGenislik = "col-xl-4 col-lg-4 col-md-12 col-sm-12 col-12";


        public static string tabloBicim1 = "table table-striped";
        public static string tabloBicim2 = "table mb-0";
        public static string tabloBicim3 = "table mb-0";


        public static string btnGridGuncelle = "btn btn-warning btn-rounded waves-effect waves-light";
        public static string btnEkle = "btn btn-success";
        public static string btnSil = "btn btn-danger btn-rounded waves-effect waves-light";


        public static string dokumGenisAramaBicim = "alert alert-warning";
        public static string dokumGenisAramaAlt = "mdi mdi-alert-circle-outline me-2";

        public static string textBoxBicim()
        {
            return "form-control";
        }

        public static string textBoxBicimKalin()
        {
            return "form-control dolgun";
        }
        public static string comboboxBicim()
        {
            return "form-control select2";
        }



        public static string dosyaYukleyiciBicim()
        {
            return "form-control";
        }


        public static string kayitBicimi(bool? durum)
        {
            if (durum == true)
                return "btn btn-success";
            else
                return "btn btn-warning";
        }
        public static string kayitIfadesi(bool? durum)
        {
            if (durum == true)
                return "Yeni Kayıt Ekle";
            else
                return "Kayıt Güncelle";
        }


        public static string yukleyiciIfadesi()
        {
            string sonuc = "tinymce.init({ selector: 'textarea ',  plugins: [ 'advlist', 'autolink', 'link', 'image', 'lists', 'charmap', 'preview', 'anchor', 'pagebreak', 'searchreplace', 'wordcount', 'visualblocks', 'visualchars', 'code', 'fullscreen', 'insertdatetime', 'media', 'table', 'emoticons', 'template', 'help' ], toolbar: 'undo redo | styles | bold italic | alignleft aligncenter alignright alignjustify | ' + 'bullist numlist outdent indent | link image | print preview media fullscreen | ' +  'forecolor backcolor emoticons | help', menu: { favs: { title: 'My Favorites', items: 'code visualaid | searchreplace | emoticons' } }, menubar: 'favs file edit view insert format tools table help', content_css: 'css/content.css', paste_data_images: true,  images_upload_url: '/GeciciDosya/Yukle/' });";

            return sonuc;
        }
    }
}
