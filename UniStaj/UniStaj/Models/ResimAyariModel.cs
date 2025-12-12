using Microsoft.EntityFrameworkCore;
using UniStaj.veri;
using UniStaj.veriTabani;


namespace UniStaj.Models
{
    public class ResimAyariModel : ModelTabani
    {
        public ResimAyari kartVerisi { get; set; }
        public List<ResimAyariAYRINTI> dokumVerisi { get; set; }
        public ResimAyariAYRINTIArama aramaParametresi { get; set; }
        public Fotograf? fotosu { get; set; }



        public ResimAyariModel()
        {
            kartVerisi = new ResimAyari();
            dokumVerisi = new List<ResimAyariAYRINTI>();
            aramaParametresi = new ResimAyariAYRINTIArama();

        }


        public AramaTalebi ayrintiliAra(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                AramaTalebi talep = new AramaTalebi();
                talep.kodu = Guid.NewGuid().ToString();
                talep.tarih = DateTime.Now;
                talep.varmi = true;
                talep.talepAyrintisi = Newtonsoft.Json.JsonConvert.SerializeObject(aramaParametresi);
                talep.kaydet(vari, false);
                return talep;
            }
        }
        public async Task yetkiKontrolu(Sayfa sayfasi)
        {
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
            if (kartVerisi._birincilAnahtar() > 0)
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)
                throw new Exception(Ikazlar.islemiYapmayaYetkinizYok(sayfasi.dilKimlik));
        }

        public string temelDosyaKonumu { get; set; }
        public string ortaDosyaKonumu { get; set; }
        public string kucukDosyaKonumu { get; set; }
        public string enkucukDosyaKonumu { get; set; }
        public async Task resimAyrintisiBelirle(Yonetici kim, long fotoKimlik)
        {
            kullanan = kim;
            using (veri.Varlik vari = new Varlik())
            {
                fotosu = await vari.Fotografs.FirstOrDefaultAsync(p => p.fotografKimlik == fotoKimlik && p.varmi == true);
                if (fotosu == null)
                    throw new Exception("Fotoðraf bulunamadý");

                ResimAyari? ayar = await vari.ResimAyaris.FirstOrDefaultAsync(P => P.ilgiliCizelge == fotosu.ilgiliCizelge && P.varmi == true);
                if (ayar == null)
                    throw new Exception("Ayar bulunamadý");

                kartVerisi = ayar;

                string temel = Path.Combine(Genel.kayitKonumu, kartVerisi.dizinAdi ?? "");
                string yeniOrta = Path.Combine(Genel.kayitKonumu, kartVerisi.dizinAdi + "\\Orta");
                string yeniKucuk = Path.Combine(Genel.kayitKonumu, kartVerisi.dizinAdi + "\\Kucuk");
                string yeniEnKucuk = Path.Combine(Genel.kayitKonumu, kartVerisi.dizinAdi + "\\EnKucuk");



                string tamDosyaAdi = fotosu.konum.Replace(fotosu.ilgiliCizelge + "\\", "");



                if (Directory.Exists(temel) == false)
                    Directory.CreateDirectory(temel);

                if (Directory.Exists(yeniOrta) == false)
                    Directory.CreateDirectory(yeniOrta);

                if (Directory.Exists(yeniKucuk) == false)
                    Directory.CreateDirectory(yeniKucuk);

                if (Directory.Exists(yeniEnKucuk) == false)
                    Directory.CreateDirectory(yeniEnKucuk);



                temelDosyaKonumu = "\\" + Path.Combine(kartVerisi.dizinAdi ?? "", tamDosyaAdi);
                ortaDosyaKonumu = "\\" + Path.Combine(kartVerisi.dizinAdi + "\\Orta", tamDosyaAdi);
                kucukDosyaKonumu = "\\" + Path.Combine(kartVerisi.dizinAdi + "\\Kucuk", tamDosyaAdi);
                enkucukDosyaKonumu = "\\" + Path.Combine(kartVerisi.dizinAdi + "\\EnKucuk", tamDosyaAdi);

            }
        }

        public void yeniAyarKaydet(veri.Varlik vari, ResimAyari yeniAyar)
        {
            string temel = Path.Combine(Genel.kayitKonumu, yeniAyar.dizinAdi ?? "");
            string yeniOrta = Path.Combine(Genel.kayitKonumu, yeniAyar.dizinAdi + "\\Orta");
            string yeniKucuk = Path.Combine(Genel.kayitKonumu, yeniAyar.dizinAdi + "\\Kucuk");
            string yeniEnKucuk = Path.Combine(Genel.kayitKonumu, yeniAyar.dizinAdi + "\\EnKucuk");

            if (Directory.Exists(temel) == false)
                Directory.CreateDirectory(temel);

            if (Directory.Exists(yeniOrta) == false)
                Directory.CreateDirectory(yeniOrta);

            if (Directory.Exists(yeniKucuk) == false)
                Directory.CreateDirectory(yeniKucuk);


            if (Directory.Exists(yeniEnKucuk) == false)
                Directory.CreateDirectory(yeniEnKucuk);

        }
        public ResimAyari kaydet(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                kartVerisi.kaydet(true);

                string temel = Path.Combine(Genel.kayitKonumu, kartVerisi.dizinAdi ?? "");
                string yeniOrta = Path.Combine(Genel.kayitKonumu, kartVerisi.dizinAdi + "\\Orta");
                string yeniKucuk = Path.Combine(Genel.kayitKonumu, kartVerisi.dizinAdi + "\\Kucuk");
                string yeniEnKucuk = Path.Combine(Genel.kayitKonumu, kartVerisi.dizinAdi + "\\EnKucuk");

                if (Directory.Exists(temel) == false)
                    Directory.CreateDirectory(temel);

                if (Directory.Exists(yeniOrta) == false)
                    Directory.CreateDirectory(yeniOrta);

                if (Directory.Exists(yeniKucuk) == false)
                    Directory.CreateDirectory(yeniKucuk);


                if (Directory.Exists(yeniEnKucuk) == false)
                    Directory.CreateDirectory(yeniEnKucuk);

                return kartVerisi;
            }
        }


        public void sil(Sayfa sayfasi, string id, Yonetici silen)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                List<string> kayitlar = id.Split(',').ToList();
                for (int i = 0; i < kayitlar.Count; i++)
                {
                    ResimAyari silinecek = ResimAyari.olustur(vari, kayitlar[i]);
                    silinecek._sayfaAta(sayfasi);
                    silinecek.sil(vari);
                }
            }
            Models.ResimAyariModel modeli = new Models.ResimAyariModel();
            modeli.veriCekKosut(silen);
        }

        public void kosulaGoreCek(Yonetici kime, string id)
        {
            kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                var talep = vari.AramaTalebis.FirstOrDefault(p => p.kodu == id);
                if (talep != null)
                {
                    //ResimAyariAYRINTIArama kosul = JsonConvert.DeserializeObject<ResimAyariAYRINTIArama>(talep.talepAyrintisi);
                    //dokumVerisi = ResimAyariAYRINTIArama.CE(vari, kosul);
                    //kartVerisi = new ResimAyari();
                    //aramaParametresi = kosul;
                }
            }
        }
        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = ResimAyari.olustur(kimlik);
            dokumVerisi = new List<ResimAyariAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new ResimAyari();
            dokumVerisi = ResimAyariAYRINTI.ara();
        }


    }
}
