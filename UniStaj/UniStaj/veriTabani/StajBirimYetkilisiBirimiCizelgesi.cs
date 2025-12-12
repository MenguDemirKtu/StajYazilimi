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

public class StajBirimYetkilisiBirimiArama
{
 public  Int32  ?  stajBirimYetkilisiBirimikimlik {get;set;}
 public  Int32  ?  i_stajBirimYetkilisiKimlik {get;set;}
 public  Int32  ?  i_stajBirimiKimlik {get;set;}
 public  bool  ?  e_gecerliMi {get;set;}
 public  bool  ?  varmi {get;set;}
 public StajBirimYetkilisiBirimiArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<StajBirimYetkilisiBirimi> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< StajBirimYetkilisiBirimi>(P => P.varmi == true);
 if (stajBirimYetkilisiBirimikimlik  != null)
 predicate = predicate.And(x => x.stajBirimYetkilisiBirimikimlik == stajBirimYetkilisiBirimikimlik ); 
 if (i_stajBirimYetkilisiKimlik  != null)
 predicate = predicate.And(x => x.i_stajBirimYetkilisiKimlik == i_stajBirimYetkilisiKimlik ); 
 if (i_stajBirimiKimlik  != null)
 predicate = predicate.And(x => x.i_stajBirimiKimlik == i_stajBirimiKimlik ); 
 if (e_gecerliMi  != null)
 predicate = predicate.And(x => x.e_gecerliMi == e_gecerliMi ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   StajBirimYetkilisiBirimi      >> cek(veri.Varlik vari)
   {
     List <StajBirimYetkilisiBirimi> sonuc = await vari.StajBirimYetkilisiBirimis
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<StajBirimYetkilisiBirimi?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    StajBirimYetkilisiBirimi ? sonuc = await vari.StajBirimYetkilisiBirimis
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class StajBirimYetkilisiBirimiCizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<StajBirimYetkilisiBirimi>> ara(params Expression<Func<StajBirimYetkilisiBirimi, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<StajBirimYetkilisiBirimi>> ara(veri.Varlik vari, params Expression<Func<StajBirimYetkilisiBirimi, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.StajBirimYetkilisiBirimis 
                  .Where(kosul).OrderByDescending(p => p.stajBirimYetkilisiBirimikimlik) 
         .ToListAsync(); 
} 



public static async Task<StajBirimYetkilisiBirimi?> tekliCekKos(Int32 kimlik, Varlik kime)
{
StajBirimYetkilisiBirimi? kayit = await kime.StajBirimYetkilisiBirimis.FirstOrDefaultAsync(p => p.stajBirimYetkilisiBirimikimlik == kimlik && p.varmi == true);
 return kayit;
}


public static async Task kaydetKos(StajBirimYetkilisiBirimi yeni, Varlik vari, params bool[] yedekAlinsinmi) 
{ 
    if (yeni.stajBirimYetkilisiBirimikimlik <= 0) 
   { 
      Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");  
     if (yeni.varmi == null)  
         yeni.varmi = true;  
      await vari.StajBirimYetkilisiBirimis.AddAsync(yeni);  
     await vari.SaveChangesAsync(); 
     await kay.kaydetKos(vari, yedekAlinsinmi); 
 } 
 else 
 { 
    Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi "); 
    StajBirimYetkilisiBirimi? bulunan = await vari.StajBirimYetkilisiBirimis.FirstOrDefaultAsync(p => p.stajBirimYetkilisiBirimikimlik == yeni.stajBirimYetkilisiBirimikimlik); 
    if (bulunan == null)  
        return;  
   vari.Entry(bulunan).CurrentValues.SetValues(yeni);  
    await vari.SaveChangesAsync();  
    await kay.kaydetKos(vari, yedekAlinsinmi);  
 } 
} 


   public static async Task silKos(StajBirimYetkilisiBirimi kimi, Varlik vari, params bool[] yedekAlinsinmi) 
  {
    Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
  kimi.varmi = false; 
  var bulunan = await vari.StajBirimYetkilisiBirimis.FirstOrDefaultAsync(p => p.stajBirimYetkilisiBirimikimlik == kimi.stajBirimYetkilisiBirimikimlik); 
  if (bulunan == null) 
      return; 
  kimi.varmi = false;
  await vari.SaveChangesAsync();
   await kay.kaydetKos(vari, yedekAlinsinmi);
 }


public static StajBirimYetkilisiBirimi? tekliCek(Int32 kimlik, Varlik kime)
{
StajBirimYetkilisiBirimi ? kayit = kime.StajBirimYetkilisiBirimis.FirstOrDefault(p => p.stajBirimYetkilisiBirimikimlik == kimlik); 
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
 public static void kaydet(StajBirimYetkilisiBirimi yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.stajBirimYetkilisiBirimikimlik <= 0  )
{
 if(yeni.varmi == null)  
   yeni.varmi = true ;       
      kime.StajBirimYetkilisiBirimis.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.StajBirimYetkilisiBirimis.FirstOrDefault(p => p.stajBirimYetkilisiBirimikimlik == yeni.stajBirimYetkilisiBirimikimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(StajBirimYetkilisiBirimi kimi, Varlik kime )
{
    kimi.varmi = false; 
 var bulunan = kime.StajBirimYetkilisiBirimis.FirstOrDefault(p => p.stajBirimYetkilisiBirimikimlik == kimi.stajBirimYetkilisiBirimikimlik);  
 if (bulunan == null) 
     return; 
  kime.Entry(bulunan).CurrentValues.SetValues(kimi);  
  kime.SaveChanges(); 
  Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi"); 
  kay.kaydet(); 

}
}
}


