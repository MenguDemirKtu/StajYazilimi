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
 public partial class StajBirimYetkilisi : Bilesen 
   { 

public StajBirimYetkilisi()
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
 return bossaDoldur( tcKimlikNo) ;
 } 



  public async static Task<StajBirimYetkilisi?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     StajBirimYetkilisi sonuc = new StajBirimYetkilisi(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.StajBirimYetkilisis.FirstOrDefaultAsync(p => p.stajBirimYetkilisikimlik == kimlik   && p.varmi == true  );
  } 
  } 


public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
if(varmi == null)
varmi = true;
 bicimlendir(vari);
  await veriTabani.StajBirimYetkilisiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
 }
 public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
    varmi = false;
    await veriTabani.StajBirimYetkilisiCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<StajBirimYetkilisi>> ara(params Expression<Func<StajBirimYetkilisi, bool>>[] kosullar)
       {
   return await veriTabani.StajBirimYetkilisiCizelgesi.ara(kosullar);
  }
        public static async Task<List<StajBirimYetkilisi>> ara(veri.Varlik vari, params Expression<Func<StajBirimYetkilisi, bool>>[] kosullar)
       {
   return await veriTabani.StajBirimYetkilisiCizelgesi.ara(vari,kosullar);
  }


    #region ozluk


public override string _cizelgeAdi()
 {
return "StajBirimYetkilisi";   
 }


   public override string _turkceAdi() 
  {
    return "Staj Birim Yetkilisi"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "stajBirimYetkilisikimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.stajBirimYetkilisikimlik;
}


    #endregion


  }
  }

