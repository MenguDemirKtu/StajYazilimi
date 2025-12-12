namespace UniStaj.veri
{
    public class KapiGecis
    {
        public string tc { get; set; }
        public string adSoyAd { get; set; }
        public string kapiAdi { get; set; }
        public DateTime tarih { get; set; }
        public string gunIfadesi
        {
            get
            {
                return tarih.ToShortDateString();
            }
        }

        public KapiGecis()
        {
            tc = "";
            adSoyAd = "";
            tarih = DateTime.Today;
        }

    }
}
