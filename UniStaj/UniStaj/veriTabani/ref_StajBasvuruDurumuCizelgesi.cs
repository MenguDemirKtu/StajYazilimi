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

public class ref_StajBasvuruDurumuArama
{
 public  Int32  ?  StajBasvuruDurumuKimlik {get;set;}
 public  string  ?  StajBasvuruDurumuAdi {get;set;}
 public ref_StajBasvuruDurumuArama()
{
}
 
        private ExpressionStarter<ref_StajBasvuruDurumu> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< ref_StajBasvuruDurumu>();
 if (StajBasvuruDurumuKimlik  != null)
 predicate = predicate.And(x => x.StajBasvuruDurumuKimlik == StajBasvuruDurumuKimlik ); 
 if (StajBasvuruDurumuAdi  != null)
               predicate = predicate.And( x => x.StajBasvuruDurumuAdi != null &&    x.StajBasvuruDurumuAdi .Contains(StajBasvuruDurumuAdi));
return  predicate;
 
}
      public async Task<List<   ref_StajBasvuruDurumu      >> cek(veri.Varlik vari)
   {
     List <ref_StajBasvuruDurumu> sonuc = await vari.ref_StajBasvuruDurumus
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<ref_StajBasvuruDurumu?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    ref_StajBasvuruDurumu ? sonuc = await vari.ref_StajBasvuruDurumus
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class ref_StajBasvuruDurumuCizelgesi
{




/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<ref_StajBasvuruDurumu>> ara(params Expression<Func<ref_StajBasvuruDurumu, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<ref_StajBasvuruDurumu>> ara(veri.Varlik vari, params Expression<Func<ref_StajBasvuruDurumu, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.ref_StajBasvuruDurumus 
                  .Where(kosul).OrderByDescending(p => p.StajBasvuruDurumuKimlik) 
         .ToListAsync(); 
} 



public static async Task<ref_StajBasvuruDurumu?> tekliCekKos(Int32 kimlik, Varlik kime)
{
ref_StajBasvuruDurumu? kayit = await kime.ref_StajBasvuruDurumus.FirstOrDefaultAsync(p => p.StajBasvuruDurumuKimlik == kimlik);
 return kayit;
}




public static ref_StajBasvuruDurumu? tekliCek(Int32 kimlik, Varlik kime)
{
ref_StajBasvuruDurumu ? kayit = kime.ref_StajBasvuruDurumus.FirstOrDefault(p => p.StajBasvuruDurumuKimlik == kimlik); 
return kayit;
}


/// <summary> 
 /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır. 
  /// </summary> 
  /// <param name="yeni"></param> 
  /// <param name="kime"></param> 
  /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param> 
 public static void kaydet(ref_StajBasvuruDurumu yeni, Varlik kime, params bool[] yedekAlinsinmi)
{
  if(yeni.StajBasvuruDurumuKimlik <= 0  )
{
          kime.ref_StajBasvuruDurumus.Add(yeni);
kime.SaveChanges();
Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
kay.kaydet(yedekAlinsinmi);
}
else
{

     var bulunan = kime.ref_StajBasvuruDurumus.FirstOrDefault(p => p.StajBasvuruDurumuKimlik == yeni.StajBasvuruDurumuKimlik);
     if (bulunan == null)
        return;
     kime.Entry(bulunan).CurrentValues.SetValues(yeni); 
kime.SaveChanges();

Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
kay.kaydet(yedekAlinsinmi);
}
}


  public static void sil(ref_StajBasvuruDurumu kimi, Varlik kime )
{

}
}
}


