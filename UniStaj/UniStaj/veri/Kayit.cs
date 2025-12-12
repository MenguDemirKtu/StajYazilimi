namespace UniStaj.veri
{
    public partial class Kayit
    {

        private long _kaydedenKisi;

        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private ISession _session => _httpContextAccessor.HttpContext.Session;
        public Kayit(Bilesen bir, string islemTuru, string ekBilgi)
        {
            try
            {
                tarih = System.DateTime.Now;
                i_kullaniciKimlik = 4;
                this.islemTuru = islemTuru;
                cizelgeAdi = bir._cizelgeAdi();
                cizelgeKimlik = bir._birincilAnahtar();
                this.ekBilgi = ekBilgi;
                ipAdresi = "...";
                kullaniciTuru = 1;

                _kaydedenKisi = bir._kullaniciDegeri();
                //string ne =                 HttpContext.Session.GetString("asd");


            }
            catch (Exception hata)
            {
                string nedir = hata.ToString();
            }



            //Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            //kay.kaydet();
        }
        public Kayit(int kullaniciKimlik, string iki, string uc)
        {

            //Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            //kay.kaydet();
        }
        public Kayit()
        {

        }
        public async Task kaydetKos(params bool[] ver)
        {
            try
            {

                bool kaydedilsinmi = true;

                if (ver.Length > 0)
                    if (ver[0] == false)
                        kaydedilsinmi = false;

                if (kaydedilsinmi)
                {

                    using (veri.Varlik vari = new Varlik())
                    {

                        i_kullaniciKimlik = (int)_kaydedenKisi;
                        if (ekBilgi != null)
                            if (ekBilgi.Length > 100)
                                ekBilgi = ekBilgi.Substring(0, 100);
                        await vari.Kayits.AddAsync(this);
                        await vari.SaveChangesAsync();
                    }
                }
            }
            catch
            {

            }
        }
        public async Task kaydetKos(veri.Varlik vari, params bool[] ver)
        {
            try
            {

                bool kaydedilsinmi = true;

                if (ver.Length > 0)
                    if (ver[0] == false)
                        kaydedilsinmi = false;

                if (kaydedilsinmi)
                {


                    i_kullaniciKimlik = (int)_kaydedenKisi;
                    if (ekBilgi != null)
                        if (ekBilgi.Length > 100)
                            ekBilgi = ekBilgi.Substring(0, 100);
                    await vari.Kayits.AddAsync(this);
                    await vari.SaveChangesAsync();
                }
            }
            catch
            {

            }
        }
        public void kaydet(params bool[] ver)
        {
            try
            {


                bool kaydedilsinmi = true;

                if (ver.Length > 0)
                    if (ver[0] == false)
                        kaydedilsinmi = false;

                if (kaydedilsinmi)
                {

                    using (veri.Varlik vari = new Varlik())
                    {

                        i_kullaniciKimlik = (int)_kaydedenKisi;
                        if (ekBilgi != null)
                            if (ekBilgi.Length > 100)
                                ekBilgi = ekBilgi.Substring(0, 100);
                        vari.Kayits.Add(this);
                        vari.SaveChanges();
                    }
                }

            }
            catch (Exception hata)
            {
                string nedir = hata.ToString();
            }
        }
        //public void kaydet()
        //{

        //}
    }
}
