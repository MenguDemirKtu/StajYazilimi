using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class ref_DilArama
    {
        public Int32? dilKimlik { get; set; }
        public string dilAdi { get; set; }
        public ref_DilArama()
        {
        }

        public Predicate<ref_Dil> kosulu()
        {
            List<Predicate<ref_Dil>> kosullar = new List<Predicate<ref_Dil>>();
            if (dilKimlik != null)
                kosullar.Add(c => c.dilKimlik == dilKimlik);
            if (dilAdi != null)
                kosullar.Add(c => c.dilAdi == dilAdi);
            Predicate<ref_Dil> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }

    }


    public class ref_DilCizelgesi
    {


        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<ref_Dil> ara(ref_DilArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return vari.ref_Dils.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.dilKimlik).ToList();
            }
        }


        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<ref_Dil> ara(params Predicate<ref_Dil>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik())
            {
                return vari.ref_Dils.ToList().FindAll(kosul).OrderByDescending(p => p.dilKimlik).ToList();
            }
        }


        public static List<ref_Dil> tamami(Varlik kime)
        {
            return kime.ref_Dils.ToList();
        }


        public static List<ref_Dil> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<ref_Dil> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static ref_Dil tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_Dil kayit = kime.ref_Dils.FirstOrDefault(p => p.dilKimlik == kimlik);
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(ref_Dil yeni, ref Varlik kime)
        {
            if (yeni.dilKimlik <= 0)
            {
                kime.ref_Dils.Add(yeni);
            }
            else
            {
                var bulunan = kime.ref_Dils.FirstOrDefault(p => p.dilKimlik == yeni.dilKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }


        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(ref_Dil yeni, Varlik kime)
        {
            kime.ref_Dils.Add(yeni);
            kime.SaveChanges();
        }


        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(ref_Dil yeni, Varlik kime)
        {
            var bulunan = kime.ref_Dils.FirstOrDefault(p => p.dilKimlik == yeni.dilKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            kime.SaveChanges();
        }


        /// <summary>
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır.
        /// </summary>
        /// <param name="yeni"></param>
        /// <param name="kime"></param>
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param>
        public static void kaydet(ref_Dil yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.dilKimlik <= 0)
            {
                kime.ref_Dils.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_Dils.FirstOrDefault(p => p.dilKimlik == yeni.dilKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_Dil kimi, Varlik kime)
        {

        }
    }
}


