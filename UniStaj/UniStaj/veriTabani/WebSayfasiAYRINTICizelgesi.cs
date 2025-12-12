using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class WebSayfasiAYRINTIArama
    {
        public Int32? webSayfasiKimlik { get; set; }
        public string? hamAdresi { get; set; }
        public string? sayfaBasligi { get; set; }
        public Int32? i_modulKimlik { get; set; }
        public string? modulAdi { get; set; }
        public Int32? i_sayfaTuruKimlik { get; set; }
        public string? tanitim { get; set; }
        public string? aciklama { get; set; }
        public bool? varmi { get; set; }
        public string? fotosu { get; set; }
        public string? sayfaTuru { get; set; }
        public bool? e_izinSayfasindaGorunsunmu { get; set; }
        public bool? e_varsayilanEklemeAcikmi { get; set; }
        public bool? e_varsayilanGorunmeAcikmi { get; set; }
        public bool? e_varsayilanGuncellemeAcikmi { get; set; }
        public bool? e_varsayilanSilmeAcikmi { get; set; }
        public string? dokumAciklamasi { get; set; }
        public string? kartAciklamasi { get; set; }
        public WebSayfasiAYRINTIArama()
        {
            this.varmi = true;
        }

        public async Task<List<WebSayfasiAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<WebSayfasiAYRINTI>(P => P.varmi == true);
            if (webSayfasiKimlik != null)
                predicate = predicate.And(x => x.webSayfasiKimlik == webSayfasiKimlik);
            if (hamAdresi != null)
                predicate = predicate.And(x => x.hamAdresi != null && x.hamAdresi.Contains(hamAdresi));
            if (sayfaBasligi != null)
                predicate = predicate.And(x => x.sayfaBasligi != null && x.sayfaBasligi.Contains(sayfaBasligi));
            if (i_modulKimlik != null)
                predicate = predicate.And(x => x.i_modulKimlik == i_modulKimlik);
            if (modulAdi != null)
                predicate = predicate.And(x => x.modulAdi != null && x.modulAdi.Contains(modulAdi));
            if (i_sayfaTuruKimlik != null)
                predicate = predicate.And(x => x.i_sayfaTuruKimlik == i_sayfaTuruKimlik);
            if (tanitim != null)
                predicate = predicate.And(x => x.tanitim != null && x.tanitim.Contains(tanitim));
            if (aciklama != null)
                predicate = predicate.And(x => x.aciklama != null && x.aciklama.Contains(aciklama));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu != null && x.fotosu.Contains(fotosu));
            if (sayfaTuru != null)
                predicate = predicate.And(x => x.sayfaTuru != null && x.sayfaTuru.Contains(sayfaTuru));
            if (e_izinSayfasindaGorunsunmu != null)
                predicate = predicate.And(x => x.e_izinSayfasindaGorunsunmu == e_izinSayfasindaGorunsunmu);
            if (e_varsayilanEklemeAcikmi != null)
                predicate = predicate.And(x => x.e_varsayilanEklemeAcikmi == e_varsayilanEklemeAcikmi);
            if (e_varsayilanGorunmeAcikmi != null)
                predicate = predicate.And(x => x.e_varsayilanGorunmeAcikmi == e_varsayilanGorunmeAcikmi);
            if (e_varsayilanGuncellemeAcikmi != null)
                predicate = predicate.And(x => x.e_varsayilanGuncellemeAcikmi == e_varsayilanGuncellemeAcikmi);
            if (e_varsayilanSilmeAcikmi != null)
                predicate = predicate.And(x => x.e_varsayilanSilmeAcikmi == e_varsayilanSilmeAcikmi);
            if (dokumAciklamasi != null)
                predicate = predicate.And(x => x.dokumAciklamasi != null && x.dokumAciklamasi.Contains(dokumAciklamasi));
            if (kartAciklamasi != null)
                predicate = predicate.And(x => x.kartAciklamasi != null && x.kartAciklamasi.Contains(kartAciklamasi));
            List<WebSayfasiAYRINTI> sonuc = new List<WebSayfasiAYRINTI>();
            sonuc = await vari.WebSayfasiAYRINTIs
        .Where(predicate)
        .ToListAsync();
            return sonuc;
        }

    }


    public class WebSayfasiAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<WebSayfasiAYRINTI>> ara(params Expression<Func<WebSayfasiAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<WebSayfasiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<WebSayfasiAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.WebSayfasiAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.webSayfasiKimlik)
                   .ToListAsync();
        }



        public static async Task<WebSayfasiAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            WebSayfasiAYRINTI? kayit = await kime.WebSayfasiAYRINTIs.FirstOrDefaultAsync(p => p.webSayfasiKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static WebSayfasiAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            WebSayfasiAYRINTI? kayit = kime.WebSayfasiAYRINTIs.FirstOrDefault(p => p.webSayfasiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

