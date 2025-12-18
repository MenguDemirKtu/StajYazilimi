using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class KullaniciRoluAYRINTIArama
    {
        public Int64? kullaniciRoluKimlik { get; set; }
        public Int32? i_kullaniciKimlik { get; set; }
        public Int32? i_rolKimlik { get; set; }
        public bool? e_gecerlimi { get; set; }
        public bool? varmi { get; set; }
        public string? rolAdi { get; set; }
        public string? kullaniciAdi { get; set; }
        public string? gercekAdi { get; set; }
        public string? ePostaAdresi { get; set; }
        public string? telefon { get; set; }
        public string? unvan { get; set; }
        public string? tcKimlikNo { get; set; }
        public string? ustTur { get; set; }
        public string? fotoBilgisi { get; set; }
        public string? ekAciklama { get; set; }
        public KullaniciRoluAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<KullaniciRoluAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<KullaniciRoluAYRINTI>(P => P.varmi == true);
            if (kullaniciRoluKimlik != null)
                predicate = predicate.And(x => x.kullaniciRoluKimlik == kullaniciRoluKimlik);
            if (i_kullaniciKimlik != null)
                predicate = predicate.And(x => x.i_kullaniciKimlik == i_kullaniciKimlik);
            if (i_rolKimlik != null)
                predicate = predicate.And(x => x.i_rolKimlik == i_rolKimlik);
            if (e_gecerlimi != null)
                predicate = predicate.And(x => x.e_gecerlimi == e_gecerlimi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (rolAdi != null)
                predicate = predicate.And(x => x.rolAdi != null && x.rolAdi.Contains(rolAdi));
            if (kullaniciAdi != null)
                predicate = predicate.And(x => x.kullaniciAdi != null && x.kullaniciAdi.Contains(kullaniciAdi));
            if (gercekAdi != null)
                predicate = predicate.And(x => x.gercekAdi != null && x.gercekAdi.Contains(gercekAdi));
            if (ePostaAdresi != null)
                predicate = predicate.And(x => x.ePostaAdresi != null && x.ePostaAdresi.Contains(ePostaAdresi));
            if (telefon != null)
                predicate = predicate.And(x => x.telefon != null && x.telefon.Contains(telefon));
            if (unvan != null)
                predicate = predicate.And(x => x.unvan != null && x.unvan.Contains(unvan));
            if (tcKimlikNo != null)
                predicate = predicate.And(x => x.tcKimlikNo != null && x.tcKimlikNo.Contains(tcKimlikNo));
            if (ustTur != null)
                predicate = predicate.And(x => x.ustTur != null && x.ustTur.Contains(ustTur));
            if (fotoBilgisi != null)
                predicate = predicate.And(x => x.fotoBilgisi != null && x.fotoBilgisi.Contains(fotoBilgisi));
            if (ekAciklama != null)
                predicate = predicate.And(x => x.ekAciklama != null && x.ekAciklama.Contains(ekAciklama));
            return predicate;

        }
        public async Task<List<KullaniciRoluAYRINTI>> cek(veri.Varlik vari)
        {
            List<KullaniciRoluAYRINTI> sonuc = await vari.KullaniciRoluAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<KullaniciRoluAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            KullaniciRoluAYRINTI? sonuc = await vari.KullaniciRoluAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class KullaniciRoluAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<KullaniciRoluAYRINTI>> ara(params Expression<Func<KullaniciRoluAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<KullaniciRoluAYRINTI>> ara(veri.Varlik vari, params Expression<Func<KullaniciRoluAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.KullaniciRoluAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.kullaniciRoluKimlik)
                   .ToListAsync();
        }



        public static async Task<KullaniciRoluAYRINTI?> tekliCekKos(Int64 kimlik, Varlik kime)
        {
            KullaniciRoluAYRINTI? kayit = await kime.KullaniciRoluAYRINTIs.FirstOrDefaultAsync(p => p.kullaniciRoluKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static KullaniciRoluAYRINTI? tekliCek(Int64 kimlik, Varlik kime)
        {
            KullaniciRoluAYRINTI? kayit = kime.KullaniciRoluAYRINTIs.FirstOrDefault(p => p.kullaniciRoluKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

