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
 public partial class StajKurumTuruAYRINTI : Bilesen 
   { 

public StajKurumTuruAYRINTI()
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
 return bossaDoldur( stajKurumTurAdi) ;
 } 



  public async static Task<StajKurumTuruAYRINTI?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     StajKurumTuruAYRINTI sonuc = new StajKurumTuruAYRINTI(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.StajKurumTuruAYRINTIs.FirstOrDefaultAsync(p => p.stajKurumTurukimlik == kimlik   && p.varmi == true  );
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

        public static async Task<List<StajKurumTuruAYRINTI>> ara(params Expression<Func<StajKurumTuruAYRINTI, bool>>[] kosullar)
       {
   return await veriTabani.StajKurumTuruAYRINTICizelgesi.ara(kosullar);
  }
        public static async Task<List<StajKurumTuruAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajKurumTuruAYRINTI, bool>>[] kosullar)
       {
   return await veriTabani.StajKurumTuruAYRINTICizelgesi.ara(vari,kosullar);
  }


    #region ozluk


public override string _cizelgeAdi()
 {
return "StajKurumTuruAYRINTI";   
 }


   public override string _turkceAdi() 
  {
    return "StajKurumTuru"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "stajKurumTurukimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.stajKurumTurukimlik;
}


    #endregion


  }
  }

