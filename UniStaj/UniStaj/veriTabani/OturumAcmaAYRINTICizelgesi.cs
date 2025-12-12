// ;
// .Sql;
// .SqlClient;
// .SqlTypes;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class OturumAcmaAYRINTIArama
    {
        public Int32? oturumAcmaKimlik { get; set; }
        public Int32? i_kullaniciKimlik { get; set; }
        public string? kullaniciAdi { get; set; }
        public string? gercekAdi { get; set; }
        public string? ePostaAdresi { get; set; }
        public string? telefon { get; set; }
        public DateTime? tarih { get; set; }
        public Int32? gunlukSayi { get; set; }
        public bool? varmi { get; set; }
        public OturumAcmaAYRINTIArama()
        {
            this.varmi = true;
        }

        public async Task<List<OturumAcmaAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<OturumAcmaAYRINTI>(P => P.varmi == true);
            if (oturumAcmaKimlik != null)
                predicate = predicate.And(x => x.oturumAcmaKimlik == oturumAcmaKimlik);
            if (i_kullaniciKimlik != null)
                predicate = predicate.And(x => x.i_kullaniciKimlik == i_kullaniciKimlik);
            if (kullaniciAdi != null)
                predicate = predicate.And(x => x.kullaniciAdi.Contains(kullaniciAdi));
            if (gercekAdi != null)
                predicate = predicate.And(x => x.gercekAdi.Contains(gercekAdi));
            if (ePostaAdresi != null)
                predicate = predicate.And(x => x.ePostaAdresi.Contains(ePostaAdresi));
            if (telefon != null)
                predicate = predicate.And(x => x.telefon.Contains(telefon));
            if (tarih != null)
                predicate = predicate.And(x => x.tarih == tarih);
            if (gunlukSayi != null)
                predicate = predicate.And(x => x.gunlukSayi == gunlukSayi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<OturumAcmaAYRINTI> sonuc = new List<OturumAcmaAYRINTI>();
            sonuc = await vari.OturumAcmaAYRINTIs
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class OturumAcmaAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<OturumAcmaAYRINTI> ara(params Predicate<OturumAcmaAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.OturumAcmaAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.oturumAcmaKimlik).ToList();
            }
        }


        public static List<OturumAcmaAYRINTI> tamami(Varlik kime)
        {
            return kime.OturumAcmaAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.oturumAcmaKimlik).ToList();
        }
        public static async Task<OturumAcmaAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            OturumAcmaAYRINTI? kayit = await kime.OturumAcmaAYRINTIs.FirstOrDefaultAsync(p => p.oturumAcmaKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static OturumAcmaAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            OturumAcmaAYRINTI kayit = kime.OturumAcmaAYRINTIs.FirstOrDefault(p => p.oturumAcmaKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

