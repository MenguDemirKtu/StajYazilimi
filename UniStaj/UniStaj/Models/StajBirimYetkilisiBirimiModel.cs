using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Web; 
using Newtonsoft.Json;
using System; 
using System.Threading; 
using Microsoft.EntityFrameworkCore; 
using System.Threading; 
using System.Collections.Generic; 
using System.Threading.Tasks; 
using UniStaj.veri; 
using UniStaj.veriTabani; 
  using Microsoft.EntityFrameworkCore;
namespace UniStaj.Models 
{ 
   public class StajBirimYetkilisiBirimiModel : ModelTabani 
   { 
       public StajBirimYetkilisiBirimi kartVerisi { get; set; } 
       public List<StajBirimYetkilisiBirimiAYRINTI> dokumVerisi { get; set; } 
       public StajBirimYetkilisiBirimiAYRINTIArama aramaParametresi { get; set; }


public StajBirimYetkilisiBirimiModel()
{
 this.kartVerisi = new StajBirimYetkilisiBirimi(); 
 this.dokumVerisi = new  List<StajBirimYetkilisiBirimiAYRINTI>(); 
 this.aramaParametresi = new StajBirimYetkilisiBirimiAYRINTIArama();
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
  public async Task silKos(Sayfa sayfasi, string id, Yonetici silen) 
  {
using (veri.Varlik vari = new veri.Varlik())
 {
    List<string> kayitlar = id.Split(',').ToList();
 for (int i = 0; i < kayitlar.Count; i++)
 {
   StajBirimYetkilisiBirimi? silinecek = await StajBirimYetkilisiBirimi.olusturKos(vari, kayitlar[i]); 
 if (silinecek == null)
    continue;
silinecek._sayfaAta(sayfasi); 
   await silinecek.silKos(vari); 
 }
 }
  Models.StajBirimYetkilisiBirimiModel modeli = new Models.StajBirimYetkilisiBirimiModel();
   await modeli.veriCekKos(silen); 
 }
 public async Task  yetkiKontrolu(Sayfa sayfasi)
  {
   enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
  if (kartVerisi._birincilAnahtar() > 0)
      yetkiTuru = enumref_YetkiTuru.Guncelleme;
   if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)
       throw new Exception(Ikazlar.islemiYapmayaYetkinizYok(sayfasi.dilKimlik));
  }




      public async Task<StajBirimYetkilisiBirimi> kaydetKos(Sayfa sayfasi)
  {
    using(veri.Varlik vari = new veri.Varlik())
  {
     kullanan = sayfasi.mevcutKullanici();
     kartVerisi._kontrolEt(sayfasi.dilKimlik, vari );
     kartVerisi._sayfaAta(sayfasi);
    await kartVerisi.kaydetKos(vari,true);
     return kartVerisi;
  }
  }


public async Task veriCekKos(Yonetici kime, long kimlik) 
   { 
   this.kullanan = kime; 
   yenimiBelirle(kimlik); 
   using (veri.Varlik vari = new Varlik()) 
 { 
    var kart = await StajBirimYetkilisiBirimi.olusturKos(vari, kimlik); 
   if (kart != null) 
       kartVerisi = kart; 
    dokumVerisi = new List<StajBirimYetkilisiBirimiAYRINTI>(); 
 await baglilariCek(vari, kime);
 } 
 } 

 public List<StajBirimYetkilisiAYRINTI> _ayStajBirimYetkilisiAYRINTI { get; set; }
 public List<StajBirimiAYRINTI> _ayStajBirimiAYRINTI { get; set; } public async Task baglilariCek(veri.Varlik vari, Yonetici kim) {   _ayStajBirimYetkilisiAYRINTI = await StajBirimYetkilisiAYRINTI.ara(vari);
   _ayStajBirimiAYRINTI = await StajBirimiAYRINTI.ara(vari);}

  public async Task veriCekKos(Yonetici kime) 
  { 
    this.kullanan = kime; 
 using (veri.Varlik vari = new Varlik()) 
 { 
     StajBirimYetkilisiBirimiAYRINTIArama kosul = new StajBirimYetkilisiBirimiAYRINTIArama(); 
     kosul.varmi = true; 
     kartVerisi = new StajBirimYetkilisiBirimi();  
     dokumVerisi = await kosul.cek(vari); 
 await baglilariCek(vari, kime); 
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
        StajBirimYetkilisiBirimiAYRINTIArama kosul = JsonConvert.DeserializeObject<StajBirimYetkilisiBirimiAYRINTIArama>(talep.talepAyrintisi?? "" )?? new StajBirimYetkilisiBirimiAYRINTIArama  ();       
       dokumVerisi = await kosul.cek(vari);      
      kartVerisi = new StajBirimYetkilisiBirimi();           
 await baglilariCek(vari, kime); 
      aramaParametresi = kosul;           
     }          
   }          
    }          

     } 
}
