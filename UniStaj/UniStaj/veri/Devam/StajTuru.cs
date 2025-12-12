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
 public partial class StajTuru : Bilesen 
   { 

public StajTuru()
{
   _varSayilan();
}


public   void bicimlendir(veri.Varlik vari) 
{

}

public   void _icDenetim(int dilKimlik, veri.Varlik vari) 
  {   
 } 


    public override string _tanimi()
  {   
 return bossaDoldur( stajTuruAdi) ;
 } 



  public async static Task<StajTuru?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     StajTuru sonuc = new StajTuru(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.StajTurus.FirstOrDefaultAsync(p => p.stajTurukimlik == kimlik   && p.varmi == true  );
  } 
  } 


public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
if(varmi == null)
varmi = true;
 bicimlendir(vari);
  await veriTabani.StajTuruCizelgesi.kaydetKos(this, vari, yedeklensinmi);
 }
 public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
    varmi = false;
    await veriTabani.StajTuruCizelgesi.silKos(this, vari, yedeklensinmi);
 } 


     public override void _kontrolEt(int dilKimlik, veri.Varlik vari) 
  { 
_icDenetim(dilKimlik, vari);
 }


       public override void _varSayilan() 
  { 
this.varmi = true;
this.varmi  =  true ; 
 }

        public static async Task<List<StajTuru>> ara(params Expression<Func<StajTuru, bool>>[] kosullar)
       {
   return await veriTabani.StajTuruCizelgesi.ara(kosullar);
  }
        public static async Task<List<StajTuru>> ara(veri.Varlik vari, params Expression<Func<StajTuru, bool>>[] kosullar)
       {
   return await veriTabani.StajTuruCizelgesi.ara(vari,kosullar);
  }


    #region ozluk


public override string _cizelgeAdi()
 {
return "StajTuru";   
 }


   public override string _turkceAdi() 
  {
    return "Staj Türü"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "stajTurukimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.stajTurukimlik;
}


    #endregion


  }
  }

