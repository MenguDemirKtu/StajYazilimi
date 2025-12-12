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
 public partial class StajBirimYetkilisiBirimi : Bilesen 
   { 

public StajBirimYetkilisiBirimi()
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
 return bossaDoldur( i_stajBirimYetkilisiKimlik) ;
 } 



  public async static Task<StajBirimYetkilisiBirimi?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     StajBirimYetkilisiBirimi sonuc = new StajBirimYetkilisiBirimi(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.StajBirimYetkilisiBirimis.FirstOrDefaultAsync(p => p.stajBirimYetkilisiBirimikimlik == kimlik   && p.varmi == true  );
  } 
  } 


public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
if(varmi == null)
varmi = true;
 bicimlendir(vari);
  await veriTabani.StajBirimYetkilisiBirimiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
 }
 public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
 {
    varmi = false;
    await veriTabani.StajBirimYetkilisiBirimiCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<StajBirimYetkilisiBirimi>> ara(params Expression<Func<StajBirimYetkilisiBirimi, bool>>[] kosullar)
       {
   return await veriTabani.StajBirimYetkilisiBirimiCizelgesi.ara(kosullar);
  }
        public static async Task<List<StajBirimYetkilisiBirimi>> ara(veri.Varlik vari, params Expression<Func<StajBirimYetkilisiBirimi, bool>>[] kosullar)
       {
   return await veriTabani.StajBirimYetkilisiBirimiCizelgesi.ara(vari,kosullar);
  }


    #region ozluk


public override string _cizelgeAdi()
 {
return "StajBirimYetkilisiBirimi";   
 }


   public override string _turkceAdi() 
  {
    return "Staj Birim Yetkilisi Birimi"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "stajBirimYetkilisiBirimikimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.stajBirimYetkilisiBirimikimlik;
}


    #endregion


  }
  }

