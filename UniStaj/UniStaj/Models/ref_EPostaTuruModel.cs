using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class ref_EPostaTuruModel : ModelTabani
    {
        public ref_EPostaTuru kartVerisi { get; set; }
        public List<ref_EPostaTuru> dokumVerisi { get; set; }
        public ref_EPostaTuruArama aramaParametresi { get; set; }


        public ref_EPostaTuruModel()
        {
            this.kartVerisi = new ref_EPostaTuru();
            this.dokumVerisi = new List<ref_EPostaTuru>();
            this.aramaParametresi = new ref_EPostaTuruArama();
        }


        public async Task<AramaTalebi> ayrintiliAraKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                AramaTalebi talep = new AramaTalebi();
                talep.kodu = Guid.NewGuid().ToString();
                talep.tarih = DateTime.Now;
                talep.varmi = true;
                talep.talepAyrintisi = Newtonsoft.Json.JsonConvert.SerializeObject(aramaParametresi);
                await veriTabani.AramaTalebiCizelgesi.kaydetKos(talep, vari, false);
                return talep;
            }
        }


        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                ref_EPostaTuruArama kosul = new ref_EPostaTuruArama();
                kartVerisi = new ref_EPostaTuru();
                dokumVerisi = await kosul.cek(vari);
            }
        }
        public async Task kosulaGoreCek(Yonetici kime, string id)
        {
            kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                var talep = vari.AramaTalebis.FirstOrDefault(p => p.kodu == id);
                if (talep != null)
                {
                    ref_EPostaTuruArama kosul = JsonConvert.DeserializeObject<ref_EPostaTuruArama>(talep.talepAyrintisi) ?? new ref_EPostaTuruArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new ref_EPostaTuru();
                    aramaParametresi = kosul;
                }
            }
        }
        public void veriCek(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = ref_EPostaTuru.olustur(kimlik);
            dokumVerisi = new List<ref_EPostaTuru>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            this.kullanan = kime;
            kartVerisi = new ref_EPostaTuru();
            dokumVerisi = ref_EPostaTuru.ara();
        }


    }
}
