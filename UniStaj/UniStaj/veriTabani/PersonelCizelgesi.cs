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

public class PersonelArama
{
 public  Int32  ?  personelKimlik {get;set;}
 public  string  ?  sicilNo {get;set;}
 public  string  ?  tcKimlikNo {get;set;}
 public  string  ?  adi {get;set;}
 public  string  ?  soyAdi {get;set;}
 public  string  ?  telefon {get;set;}
 public  string  ?  ePosta {get;set;}
 public  Int64  ?  y_kisiKimlik {get;set;}
 public  bool  ?  varmi {get;set;}
 public  Int32  ?  i_cinsiyetKimlik {get;set;}
 public  string  ?  pasaportNo {get;set;}
 public  DateTime   ?  dogumTarihi {get;set;}
 public  string  ?  anaAdi {get;set;}
 public  string  ?  babaAdi {get;set;}
 public PersonelArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<Personel> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< Personel>(P => P.varmi == true);
 if (personelKimlik  != null)
 predicate = predicate.And(x => x.personelKimlik == personelKimlik ); 
 if (sicilNo  != null)
               predicate = predicate.And( x => x.sicilNo != null &&    x.sicilNo .Contains(sicilNo));
 if (tcKimlikNo  != null)
               predicate = predicate.And( x => x.tcKimlikNo != null &&    x.tcKimlikNo .Contains(tcKimlikNo));
 if (adi  != null)
               predicate = predicate.And( x => x.adi != null &&    x.adi .Contains(adi));
 if (soyAdi  != null)
               predicate = predicate.And( x => x.soyAdi != null &&    x.soyAdi .Contains(soyAdi));
 if (telefon  != null)
               predicate = predicate.And( x => x.telefon != null &&    x.telefon .Contains(telefon));
 if (ePosta  != null)
               predicate = predicate.And( x => x.ePosta != null &&    x.ePosta .Contains(ePosta));
 if (y_kisiKimlik  != null)
 predicate = predicate.And(x => x.y_kisiKimlik == y_kisiKimlik ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
 if (i_cinsiyetKimlik  != null)
 predicate = predicate.And(x => x.i_cinsiyetKimlik == i_cinsiyetKimlik ); 
 if (pasaportNo  != null)
               predicate = predicate.And( x => x.pasaportNo != null &&    x.pasaportNo .Contains(pasaportNo));
 if (dogumTarihi  != null)
 predicate = predicate.And(x => x.dogumTarihi == dogumTarihi ); 
 if (anaAdi  != null)
               predicate = predicate.And( x => x.anaAdi != null &&    x.anaAdi .Contains(anaAdi));
 if (babaAdi  != null)
               predicate = predicate.And( x => x.babaAdi != null &&    x.babaAdi .Contains(babaAdi));
return  predicate;
 
}
      public async Task<List<   Personel      >> cek(veri.Varlik vari)
   {
     List <Personel> sonuc = await vari.Personels
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<Personel?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    Personel ? sonuc = await vari.Personels
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class PersonelCizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<Personel>> ara(params Expression<Func<Personel, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<Personel>> ara(veri.Varlik vari, params Expression<Func<Personel, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.Personels 
                  .Where(kosul).OrderByDescending(p => p.personelKimlik) 
         .ToListAsync(); 
} 



public static async Task<Personel?> tekliCekKos(Int32 kimlik, Varlik kime)
{
Personel? kayit = await kime.Personels.FirstOrDefaultAsync(p => p.personelKimlik == kimlik && p.varmi == true);
 return kayit;
}


public static async Task kaydetKos(Personel yeni, Varlik vari, params bool[] yedekAlinsinmi) 
{ 
    if (yeni.personelKimlik <= 0) 
   { 
      Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");  
     if (yeni.varmi == null)  
         yeni.varmi = true;  
      await vari.Personels.AddAsync(yeni);  
     await vari.SaveChangesAsync(); 
     await kay.kaydetKos(vari, yedekAlinsinmi); 
 } 
 else 
 { 
    Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi "); 
    Personel? bulunan = await vari.Personels.FirstOrDefaultAsync(p => p.personelKimlik == yeni.personelKimlik); 
    if (bulunan == null)  
        return;  
   vari.Entry(bulunan).CurrentValues.SetValues(yeni);  
    await vari.SaveChangesAsync();  
    await kay.kaydetKos(vari, yedekAlinsinmi);  
 } 
} 


   public static async Task silKos(Personel kimi, Varlik vari, params bool[] yedekAlinsinmi) 
  {
    Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
  kimi.varmi = false; 
  var bulunan = await vari.Personels.FirstOrDefaultAsync(p => p.personelKimlik == kimi.personelKimlik); 
  if (bulunan == null) 
      return; 
  kimi.varmi = false;
  await vari.SaveChangesAsync();
   await kay.kaydetKos(vari, yedekAlinsinmi);
 }


public static Personel? tekliCek(Int32 kimlik, Varlik kime)
{
Personel ? kayit = kime.Personels.FirstOrDefault(p => p.personelKimlik == kimlik); 
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
 public static void kaydet(Personel yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.personelKimlik <= 0  )
{
 if(yeni.varmi == null)  
   yeni.varmi = true ;       
      kime.Personels.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.Personels.FirstOrDefault(p => p.personelKimlik == yeni.personelKimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(Personel kimi, Varlik kime )
{
    kimi.varmi = false; 
 var bulunan = kime.Personels.FirstOrDefault(p => p.personelKimlik == kimi.personelKimlik);  
 if (bulunan == null) 
     return; 
  kime.Entry(bulunan).CurrentValues.SetValues(kimi);  
  kime.SaveChanges(); 
  Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi"); 
  kay.kaydet(); 

}
}
}


