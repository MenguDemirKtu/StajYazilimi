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

public class StajTuruAYRINTIArama
{
 public  Int32  ?  stajTurukimlik {get;set;}
 public  string  ?  stajTuruAdi {get;set;}
 public  string  ?  tanim {get;set;}
 public  bool  ?  varmi {get;set;}
 public StajTuruAYRINTIArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<StajTuruAYRINTI> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< StajTuruAYRINTI>(P => P.varmi == true);
 if (stajTurukimlik  != null)
 predicate = predicate.And(x => x.stajTurukimlik == stajTurukimlik ); 
 if (stajTuruAdi  != null)
               predicate = predicate.And( x => x.stajTuruAdi != null &&    x.stajTuruAdi .Contains(stajTuruAdi));
 if (tanim  != null)
               predicate = predicate.And( x => x.tanim != null &&    x.tanim .Contains(tanim));
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   StajTuruAYRINTI      >> cek(veri.Varlik vari)
   {
     List <StajTuruAYRINTI> sonuc = await vari.StajTuruAYRINTIs
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<StajTuruAYRINTI?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    StajTuruAYRINTI ? sonuc = await vari.StajTuruAYRINTIs
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class StajTuruAYRINTICizelgesi
{





/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<StajTuruAYRINTI>> ara(params Expression<Func<StajTuruAYRINTI, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<StajTuruAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajTuruAYRINTI, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.StajTuruAYRINTIs 
                  .Where(kosul).OrderByDescending(p => p.stajTurukimlik) 
         .ToListAsync(); 
} 



public static async Task<StajTuruAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
{
StajTuruAYRINTI? kayit = await kime.StajTuruAYRINTIs.FirstOrDefaultAsync(p => p.stajTurukimlik == kimlik && p.varmi == true);
 return kayit;
}




public static StajTuruAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
{
StajTuruAYRINTI ? kayit = kime.StajTuruAYRINTIs.FirstOrDefault(p => p.stajTurukimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}
}
}

