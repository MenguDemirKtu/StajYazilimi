using Microsoft.AspNetCore.Mvc.Rendering; 
using Microsoft.EntityFrameworkCore; 
using System; 
using System.Threading; 
using Microsoft.EntityFrameworkCore; 
using System.Threading; 
using System.Linq.Expressions;
using System.Collections.Generic; 
using System.Threading.Tasks; 
using System.ComponentModel.DataAnnotations.Schema;
using UniStaj.veriTabani;

namespace UniStaj.veri
  { 
 public partial class StajBirimiTurleriAYRINTI : Bilesen 
   { 

public StajBirimiTurleriAYRINTI()
{
   _varSayilan();
}


public   void bicimlendir(veri.Varlik vari) 
{

}

public   void _icDenetim(int dilKimlik, veri.Varlik vari) 
  {   
uyariVerInt32(i_stajBirimiKimlik,"." , dilKimlik ) ; 
uyariVerInt32(i_stajTuruKimlik,"." , dilKimlik ) ; 
 } 


    public override string _tanimi()
  {   
 return bossaDoldur( i_stajBirimiKimlik) ;
 } 



  public async static Task<StajBirimiTurleriAYRINTI?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     StajBirimiTurleriAYRINTI sonuc = new StajBirimiTurleriAYRINTI(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.StajBirimiTurleriAYRINTIs.FirstOrDefaultAsync(p => p.stajBirimiTurlerikimlik == kimlik   && p.varmi == true  );
  } 
  } 


     public override void _kontrolEt(int dilKimlik, veri.Varlik vari) 
  { 
_icDenetim(dilKimlik, vari);
 }


       public override void _varSayilan() 
  { 
this.varmi = true;
 }

        public static async Task<List<StajBirimiTurleriAYRINTI>> ara(params Expression<Func<StajBirimiTurleriAYRINTI, bool>>[] kosullar)
       {
   return await veriTabani.StajBirimiTurleriAYRINTICizelgesi.ara(kosullar);
  }
        public static async Task<List<StajBirimiTurleriAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajBirimiTurleriAYRINTI, bool>>[] kosullar)
       {
   return await veriTabani.StajBirimiTurleriAYRINTICizelgesi.ara(vari,kosullar);
  }


    #region ozluk


public override string _cizelgeAdi()
 {
return "StajBirimiTurleriAYRINTI";   
 }


   public override string _turkceAdi() 
  {
    return "StajBirimi"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "stajBirimiTurlerikimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.stajBirimiTurlerikimlik;
}


    #endregion


  }
  }

