using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj
{
    public class Yonetici
    {
        public string fotosu { get; set; }
        public string gercekAdi { get; set; }
        public int kullaniciKimlik { get; set; }
        public string kullaniciAdi { get; set; }
        public string tcKimlikNo { get; set; }
        public string unvan { get; set; }
        public string yoneticiTuru { get; set; }

        public string ogrenciNo { get; set; }

        public string epostaAdresi { get; set; }
        public List<veri.KullaniciBildirimi> _bildirimler { get; set; }


        public int i_stajyerKimlik { get; set; }

        public enumref_KullaniciTuru _KullaniciTuru
        {
            get
            {
                return (enumref_KullaniciTuru)i_kullaniciTuruKimlik;
            }
        }


        public int i_kullaniciTuruKimlik { get; set; }

        public int dilKimlik { get; set; }

        public bool e_rolTabanlimi { get; set; }

        public bool e_lisansliSporcumu { get; set; }
        public enumref_KullaniciTuru _turu()
        {
            return (enumref_KullaniciTuru)i_kullaniciTuruKimlik;
        }

        public string kullaniciTuruFotosu { get; set; }
        public bool kisitliGosterimmi { get; set; }
        public int i_rolKimlik { get; set; }
        public int i_kisiTuruKimlik { get; set; }
        public int[] rolleri { get; set; }

        public bool e_sifreDegisecekmi { get; set; }

        public bool e_sozlesmeOnaylandimi { get; set; }

        public bool e_kodOnaylandimi { get; set; }

        public int ekKullaniciSayisi { get; set; }
        public Yonetici()
        {
            rolleri = new int[10];
            kisitliGosterimmi = false;
            e_rolTabanlimi = false;
            kullaniciKimlik = 0;
            _bildirimler = new List<veri.KullaniciBildirimi>();
            e_sifreDegisecekmi = false;
            fotosu = "";
            gercekAdi = "";
            kullaniciAdi = "";
            tcKimlikNo = "";
            unvan = "";
            yoneticiTuru = "";
            ogrenciNo = "";
            epostaAdresi = "";
            kullaniciTuruFotosu = "";
            e_sozlesmeOnaylandimi = false;
            e_kodOnaylandimi = false;
            ekKullaniciSayisi = 0;
            i_stajyerKimlik = 0;
        }



        /// <summary>
        /// Kullanıcını rol işlemi olup olmadığını belirler.
        /// </summary>
        /// <param name="vari"></param>
        /// <param name="rolIslemi"></param>
        /// <returns></returns>
        public async Task<bool> rolIslemiVarmi(veri.Varlik vari, enumref_RolIslemi rolIslemi)
        {

            RolAYRINTI? rolu = await vari.RolAYRINTIs.FirstOrDefaultAsync(p => p.e_gecerlimi == true
                                    && p.e_rolIslemiIcinmi == true
                                    && p.i_rolIslemiKimlik == (int)rolIslemi);

            if (rolu != null)
            {
                KullaniciRoluAYRINTI? karsilik = await vari.KullaniciRoluAYRINTIs.FirstOrDefaultAsync(p => p.i_rolKimlik == rolu.rolKimlik
                                          && p.i_kullaniciKimlik == kullaniciKimlik);
                if (karsilik == null)
                    return false;
                else
                    return true;
            }

            return false;
        }





    }
}
