namespace UniStaj.Models
{
    public class TekliSMSGonderimModel : ModelTabani
    {
        public string telNolar { get; set; }
        public string metin { get; set; }

        public TekliSMSGonderimModel()
        {
            telNolar = "";
            metin = "";
        }

        public async Task<int> gonderKos()
        {
            int sayi = 0;
            using (veri.Varlik vari = new veri.Varlik())
            {
                string[] numa = telNolar.Split(',', ';');

                for (int i = 0; i < numa.Length; i++)
                {
                    if (string.IsNullOrEmpty(numa[i]))
                        continue;
                    string tel = Genel.telNoBicimlendir(numa[i]);
                    if (tel == ".")
                        continue;
                    await GenelIslemler.SMSIslemi.smsGonder(vari, tel, metin);
                    sayi++;
                }
            }
            return sayi;
        }
    }
}
