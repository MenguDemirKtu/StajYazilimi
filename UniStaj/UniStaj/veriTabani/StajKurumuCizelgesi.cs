using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniStaj.veri;
using System; 
using LinqKit;
using System.Threading;
using System.Collections;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veriTabani
{

public class StajKurumuArama
{
 public  Int32  ?  stajKurumukimlik {get;set;}
 public  string  ?  stajKurumAdi {get;set;}
 public  Int32  ?  i_stajkurumturukimlik {get;set;}
 public  string  ?  hizmetAlani {get;set;}
 public  string  ?  webAdresi {get;set;}
 public  string  ?  vergiNo {get;set;}
 public  string  ?  ibanNo {get;set;}
 public  Int32  ?  calisanSayisi {get;set;}
 public  string  ?  adresi {get;set;}
 public  string  ?  telNo {get;set;}
 public  string  ?  ePosta {get;set;}
 public  string  ?  faks {get;set;}
 public  bool  ?  e_karaListedeMi {get;set;}
 public  bool  ?  varmi {get;set;}
 public StajKurumuArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<StajKurumu> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< StajKurumu>(P => P.varmi == true);
 if (stajKurumukimlik  != null)
 predicate = predicate.And(x => x.stajKurumukimlik == stajKurumukimlik ); 
 if (stajKurumAdi  != null)
               predicate = predicate.And( x => x.stajKurumAdi != null &&    x.stajKurumAdi .Contains(stajKurumAdi));
 if (i_stajkurumturukimlik  != null)
 predicate = predicate.And(x => x.i_stajkurumturukimlik == i_stajkurumturukimlik ); 
 if (hizmetAlani  != null)
               predicate = predicate.And( x => x.hizmetAlani != null &&    x.hizmetAlani .Contains(hizmetAlani));
 if (webAdresi  != null)
               predicate = predicate.And( x => x.webAdresi != null &&    x.webAdresi .Contains(webAdresi));
 if (vergiNo  != null)
               predicate = predicate.And( x => x.vergiNo != null &&    x.vergiNo .Contains(vergiNo));
 if (ibanNo  != null)
               predicate = predicate.And( x => x.ibanNo != null &&    x.ibanNo .Contains(ibanNo));
 if (calisanSayisi  != null)
 predicate = predicate.And(x => x.calisanSayisi == calisanSayisi ); 
 if (adresi  != null)
               predicate = predicate.And( x => x.adresi != null &&    x.adresi .Contains(adresi));
 if (telNo  != null)
               predicate = predicate.And( x => x.telNo != null &&    x.telNo .Contains(telNo));
 if (ePosta  != null)
               predicate = predicate.And( x => x.ePosta != null &&    x.ePosta .Contains(ePosta));
 if (faks  != null)
               predicate = predicate.And( x => x.faks != null &&    x.faks .Contains(faks));
 if (e_karaListedeMi  != null)
 predicate = predicate.And(x => x.e_karaListedeMi == e_karaListedeMi ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   StajKurumu      >> cek(veri.Varlik vari)
   {
     List <StajKurumu> sonuc = await vari.StajKurumus
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<StajKurumu?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    StajKurumu ? sonuc = await vari.StajKurumus
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class StajKurumuCizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<StajKurumu>> ara(params Expression<Func<StajKurumu, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<StajKurumu>> ara(veri.Varlik vari, params Expression<Func<StajKurumu, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.StajKurumus 
                  .Where(kosul).OrderByDescending(p => p.stajKurumukimlik) 
         .ToListAsync(); 
} 



public static async Task<StajKurumu?> tekliCekKos(Int32 kimlik, Varlik kime)
{
StajKurumu? kayit = await kime.StajKurumus.FirstOrDefaultAsync(p => p.stajKurumukimlik == kimlik && p.varmi == true);
 return kayit;
}


public static async Task kaydetKos(StajKurumu yeni, Varlik vari, params bool[] yedekAlinsinmi) 
{ 
    if (yeni.stajKurumukimlik <= 0) 
   { 
      Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");  
     if (yeni.varmi == null)  
         yeni.varmi = true;  
      await vari.StajKurumus.AddAsync(yeni);  
     await vari.SaveChangesAsync(); 
     await kay.kaydetKos(vari, yedekAlinsinmi); 
 } 
 else 
 { 
    Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi "); 
    StajKurumu? bulunan = await vari.StajKurumus.FirstOrDefaultAsync(p => p.stajKurumukimlik == yeni.stajKurumukimlik); 
    if (bulunan == null)  
        return;  
   vari.Entry(bulunan).CurrentValues.SetValues(yeni);  
    await vari.SaveChangesAsync();  
    await kay.kaydetKos(vari, yedekAlinsinmi);  
 } 
} 


   public static async Task silKos(StajKurumu kimi, Varlik vari, params bool[] yedekAlinsinmi) 
  {
    Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
  kimi.varmi = false; 
  var bulunan = await vari.StajKurumus.FirstOrDefaultAsync(p => p.stajKurumukimlik == kimi.stajKurumukimlik); 
  if (bulunan == null) 
      return; 
  kimi.varmi = false;
  await vari.SaveChangesAsync();
   await kay.kaydetKos(vari, yedekAlinsinmi);
 }


public static StajKurumu? tekliCek(Int32 kimlik, Varlik kime)
{
StajKurumu ? kayit = kime.StajKurumus.FirstOrDefault(p => p.stajKurumukimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}


/// <summary> 
 /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır. 
  /// </summary> 
  /// <param name="yeni"></param> 
  /// <param name="kime"></param> 
  /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param> 
 public static void kaydet(StajKurumu yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.stajKurumukimlik <= 0  )
{
 if(yeni.varmi == null)  
   yeni.varmi = true ;       
      kime.StajKurumus.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.StajKurumus.FirstOrDefault(p => p.stajKurumukimlik == yeni.stajKurumukimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(StajKurumu kimi, Varlik kime )
{
    kimi.varmi = false; 
 var bulunan = kime.StajKurumus.FirstOrDefault(p => p.stajKurumukimlik == kimi.stajKurumukimlik);  
 if (bulunan == null) 
     return; 
  kime.Entry(bulunan).CurrentValues.SetValues(kimi);  
  kime.SaveChanges(); 
  Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi"); 
  kay.kaydet(); 

}
}
}


