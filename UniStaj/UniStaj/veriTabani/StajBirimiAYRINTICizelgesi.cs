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

public class StajBirimiAYRINTIArama
{
 public  Int32  ?  stajBirimikimlik {get;set;}
 public  string  ?  stajBirimAdi {get;set;}
 public  string  ?  telefon {get;set;}
 public  string  ?  ePosta {get;set;}
 public  string  ?  birimSorumluAdi {get;set;}
 public  bool  ?  e_altBirimMi {get;set;}
 public  string  ?  altBirimMi {get;set;}
 public  Int32  ?  i_ustStajBirimiKimlik {get;set;}
 public  bool  ?  varmi {get;set;}
 public StajBirimiAYRINTIArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<StajBirimiAYRINTI> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< StajBirimiAYRINTI>(P => P.varmi == true);
 if (stajBirimikimlik  != null)
 predicate = predicate.And(x => x.stajBirimikimlik == stajBirimikimlik ); 
 if (stajBirimAdi  != null)
               predicate = predicate.And( x => x.stajBirimAdi != null &&    x.stajBirimAdi .Contains(stajBirimAdi));
 if (telefon  != null)
               predicate = predicate.And( x => x.telefon != null &&    x.telefon .Contains(telefon));
 if (ePosta  != null)
               predicate = predicate.And( x => x.ePosta != null &&    x.ePosta .Contains(ePosta));
 if (birimSorumluAdi  != null)
               predicate = predicate.And( x => x.birimSorumluAdi != null &&    x.birimSorumluAdi .Contains(birimSorumluAdi));
 if (e_altBirimMi  != null)
 predicate = predicate.And(x => x.e_altBirimMi == e_altBirimMi ); 
 if (altBirimMi  != null)
               predicate = predicate.And( x => x.altBirimMi != null &&    x.altBirimMi .Contains(altBirimMi));
 if (i_ustStajBirimiKimlik  != null)
 predicate = predicate.And(x => x.i_ustStajBirimiKimlik == i_ustStajBirimiKimlik ); 
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   StajBirimiAYRINTI      >> cek(veri.Varlik vari)
   {
     List <StajBirimiAYRINTI> sonuc = await vari.StajBirimiAYRINTIs
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<StajBirimiAYRINTI?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    StajBirimiAYRINTI ? sonuc = await vari.StajBirimiAYRINTIs
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class StajBirimiAYRINTICizelgesi
{





/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<StajBirimiAYRINTI>> ara(params Expression<Func<StajBirimiAYRINTI, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<StajBirimiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajBirimiAYRINTI, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.StajBirimiAYRINTIs 
                  .Where(kosul).OrderByDescending(p => p.stajBirimikimlik) 
         .ToListAsync(); 
} 



public static async Task<StajBirimiAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
{
StajBirimiAYRINTI? kayit = await kime.StajBirimiAYRINTIs.FirstOrDefaultAsync(p => p.stajBirimikimlik == kimlik && p.varmi == true);
 return kayit;
}




public static StajBirimiAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
{
StajBirimiAYRINTI ? kayit = kime.StajBirimiAYRINTIs.FirstOrDefault(p => p.stajBirimikimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}
}
}

