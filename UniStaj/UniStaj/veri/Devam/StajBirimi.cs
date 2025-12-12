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
 public partial class StajBirimi : Bilesen 
   { 

public StajBirimi()
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
 return bossaDoldur( stajBirimAdi) ;
 } 



  public async static Task<StajBirimi?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     StajBirimi sonuc = new StajBirimi(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.StajBirimis.FirstOrDefaultAsync(p => p.stajBirimikimlik == kimlik   && p.varmi == true  );
  } 
  } 


public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
if(varmi == null)
varmi = true;
 bicimlendir(vari);
  await veriTabani.StajBirimiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
 }
 public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
    varmi = false;
    await veriTabani.StajBirimiCizelgesi.silKos(this, vari, yedeklensinmi);
 } 


     public override void _kontrolEt(int dilKimlik, veri.Varlik vari) 
  { 
_icDenetim(dilKimlik, vari);
 }


       public override void _varSayilan() 
  { 
this.varmi = true;
this.e_altBirimMi  =  false ; 
this.varmi  =  true ; 
 }

        public static async Task<List<StajBirimi>> ara(params Expression<Func<StajBirimi, bool>>[] kosullar)
       {
   return await veriTabani.StajBirimiCizelgesi.ara(kosullar);
  }
        public static async Task<List<StajBirimi>> ara(veri.Varlik vari, params Expression<Func<StajBirimi, bool>>[] kosullar)
       {
   return await veriTabani.StajBirimiCizelgesi.ara(vari,kosullar);
  }


    #region ozluk


public override string _cizelgeAdi()
 {
return "StajBirimi";   
 }


   public override string _turkceAdi() 
  {
    return "Staj Birimi"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "stajBirimikimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.stajBirimikimlik;
}


    #endregion


  }
  }

