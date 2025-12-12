using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.Models
{
    public class DuyuruModel : ModelTabani
    {
        public Duyuru kartVerisi { get; set; }
        public List<DuyuruAYRINTI> dokumVerisi { get; set; }
        public int[] rolleri { get; set; }


        public DuyuruModel()
        {
            kartVerisi = new Duyuru();
            dokumVerisi = new List<DuyuruAYRINTI>();
            rolleri = new int[0];
        }


        public async Task yetkiKontrolu(Sayfa sayfasi)
        {
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
            if (kartVerisi._birincilAnahtar() > 0)
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)
            {
                throw new Exception(Ikazlar.islemiYapmayaYetkinizYok(sayfasi.dilKimlik));
            }
        }


        public async Task<Duyuru> kaydet(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                if (kartVerisi == null)
                    throw new Exception("Yanlýþ parametre");
                kartVerisi.aciklama = Sayfa.dosyaKonumDuzelt(kartVerisi.aciklama ?? "", Genel.yazilimAyari);
                kartVerisi._sayfaAta(sayfasi);
                kartVerisi.kaydet(true);

                List<Duyuru> liste = await vari.Duyurus.OrderBy(p => p.sirasi).ToListAsync();
                for (int i = 0; i < liste.Count; i++)
                {
                    liste[i].sirasi = (i + 1) * 5;
                    liste[i].kaydet(true, false);
                }


                List<DuyuruRolBagi> eskiler = vari.DuyuruRolBagis.Where(p => p.i_duyuruKimlik == kartVerisi.duyurukimlik).ToList();
                foreach (var siradaki in eskiler)
                {
                    siradaki.varmi = false;
                    siradaki.kaydet(vari, false);
                }

                for (int i = 0; i < rolleri.Length; i++)
                {
                    DuyuruRolBagi bag = new DuyuruRolBagi();
                    bag.varmi = true;
                    bag.i_duyuruKimlik = kartVerisi.duyurukimlik;
                    bag.i_rolKimlik = rolleri[i];
                    bag.kaydet(vari, false);
                }

                return kartVerisi;
            }
        }


        public void sil(Sayfa sayfasi, string? id, Yonetici silen)
        {
            if (id == null)
                throw new ArgumentNullException("id");
            using (veri.Varlik vari = new veri.Varlik())
            {
                List<string> kayitlar = id.Split(',').ToList();
                for (int i = 0; i < kayitlar.Count; i++)
                {
                    Duyuru silinecek = Duyuru.olustur(vari, kayitlar[i]);
                    silinecek._sayfaAta(sayfasi);
                    silinecek.sil(vari);
                }
            }
            Models.DuyuruModel modeli = new Models.DuyuruModel();
            modeli.veriCekKosut(silen);
        }

        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = Duyuru.olustur(kimlik);
            dokumVerisi = new List<DuyuruAYRINTI>();
            using (veri.Varlik vari = new Varlik())
            {
                int i = 0;
                List<DuyuruRolBagi> eskiler = vari.DuyuruRolBagis.Where(p => p.i_duyuruKimlik == kartVerisi.duyurukimlik).ToList();
                rolleri = new int[eskiler.Count];
                foreach (var siradaki in eskiler)
                {
                    rolleri[i] = siradaki.i_rolKimlik;
                    i++;
                }
            }
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new Duyuru();
            dokumVerisi = DuyuruAYRINTI.ara();
        }


    }
}
