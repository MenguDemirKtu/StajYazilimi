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

public class StajBirimYetkilisiBirimiAYRINTIArama
{
 public  Int32  ?  stajBirimYetkilisiBirimikimlik {get;set;}
 public  Int32  ?  i_stajBirimYetkilisiKimlik {get;set;}
 public  string  ?  tcKimlikNo {get;set;}
 public  Int32  ?  i_stajBirimiKimlik {get;set;}
 public  string  ?  stajBirimAdi {get;set;}
 public  bool  ?  e_gecerliMi {get;set;}
 public  string  ?  gecerliMi {get;set;}
 public  bool  ?  varmi {get;set;}
 public StajBirimYetkilisiBirimiAYRINTIArama()
{
this.varmi = true;
}
 
        private ExpressionStarter<StajBirimYetkilisiBirimiAYRINTI> kosulOlustur()
   {  
  var predicate = PredicateBuilder.New< StajBirimYetkilisiBirimiAYRINTI>(P => P.varmi == true);
 if (stajBirimYetkilisiBirimikimlik  != null)
 predicate = predicate.And(x => x.stajBirimYetkilisiBirimikimlik == stajBirimYetkilisiBirimikimlik ); 
 if (i_stajBirimYetkilisiKimlik  != null)
 predicate = predicate.And(x => x.i_stajBirimYetkilisiKimlik == i_stajBirimYetkilisiKimlik ); 
 if (tcKimlikNo  != null)
               predicate = predicate.And( x => x.tcKimlikNo != null &&    x.tcKimlikNo .Contains(tcKimlikNo));
 if (i_stajBirimiKimlik  != null)
 predicate = predicate.And(x => x.i_stajBirimiKimlik == i_stajBirimiKimlik ); 
 if (stajBirimAdi  != null)
               predicate = predicate.And( x => x.stajBirimAdi != null &&    x.stajBirimAdi .Contains(stajBirimAdi));
 if (e_gecerliMi  != null)
 predicate = predicate.And(x => x.e_gecerliMi == e_gecerliMi ); 
 if (gecerliMi  != null)
               predicate = predicate.And( x => x.gecerliMi != null &&    x.gecerliMi .Contains(gecerliMi));
 if (varmi  != null)
 predicate = predicate.And(x => x.varmi == varmi ); 
return  predicate;
 
}
      public async Task<List<   StajBirimYetkilisiBirimiAYRINTI      >> cek(veri.Varlik vari)
   {
     List <StajBirimYetkilisiBirimiAYRINTI> sonuc = await vari.StajBirimYetkilisiBirimiAYRINTIs
    .Where(kosulOlustur())
    .ToListAsync();
      return sonuc;
   }
   public async Task<StajBirimYetkilisiBirimiAYRINTI?> bul(veri.Varlik vari)
   {
    var predicate = kosulOlustur();
    StajBirimYetkilisiBirimiAYRINTI ? sonuc = await vari.StajBirimYetkilisiBirimiAYRINTIs
   .Where(predicate)
   .FirstOrDefaultAsync();
    return sonuc;
   } 
   } 


  public class StajBirimYetkilisiBirimiAYRINTICizelgesi
{





/// <summary> 
/// Girilen koşullara göre veri çeker. 
/// </summary>  
/// <param name="kosullar"></param> 
/// <returns></returns> 
        public static async Task<List<StajBirimYetkilisiBirimiAYRINTI>> ara(params Expression<Func<StajBirimYetkilisiBirimiAYRINTI, bool>>[] kosullar) 
  { 
   using (var vari = new veri.Varlik()) 
  {  
      return await ara(vari, kosullar); 
  } 
 } 
public static async Task<List<StajBirimYetkilisiBirimiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajBirimYetkilisiBirimiAYRINTI, bool>>[] kosullar)  
{ 
  var kosul = Vt.Birlestir(kosullar); 
  return await vari.StajBirimYetkilisiBirimiAYRINTIs 
                  .Where(kosul).OrderByDescending(p => p.stajBirimYetkilisiBirimikimlik) 
         .ToListAsync(); 
} 



public static async Task<StajBirimYetkilisiBirimiAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
{
StajBirimYetkilisiBirimiAYRINTI? kayit = await kime.StajBirimYetkilisiBirimiAYRINTIs.FirstOrDefaultAsync(p => p.stajBirimYetkilisiBirimikimlik == kimlik && p.varmi == true);
 return kayit;
}




public static StajBirimYetkilisiBirimiAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
{
StajBirimYetkilisiBirimiAYRINTI ? kayit = kime.StajBirimYetkilisiBirimiAYRINTIs.FirstOrDefault(p => p.stajBirimYetkilisiBirimikimlik == kimlik); 
 if (kayit != null)   
    if (kayit.varmi != true) 
      return null; 
return kayit;
}
}
}

