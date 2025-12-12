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
 public partial class PersonelAYRINTI : Bilesen 
   { 

public PersonelAYRINTI()
{
   _varSayilan();
}


public   void bicimlendir(veri.Varlik vari) 
{

}

public   void _icDenetim(int dilKimlik, veri.Varlik vari) 
  {   
uyariVerString(CinsiyetAdi,"." , dilKimlik ) ; 
 } 




    [NotMapped]
        public enumref_Cinsiyet _Cinsiyet
        {
            get
            {
                return (enumref_Cinsiyet)this.i_cinsiyetKimlik;
            }
            set
            {
              i_cinsiyetKimlik = (int)value;
            }
        }


    public override string _tanimi()
  {   
 return bossaDoldur( sicilNo) ;
 } 



  public async static Task<PersonelAYRINTI?> olusturKos(Varlik vari, object deger)
  {
    Int64 kimlik = Convert.ToInt64(deger);
   if (kimlik <= 0)
   {
     PersonelAYRINTI sonuc = new PersonelAYRINTI(); 
     sonuc._varSayilan();
      return sonuc;
   }
  else
  {
      return await vari.PersonelAYRINTIs.FirstOrDefaultAsync(p => p.personelKimlik == kimlik   && p.varmi == true  );
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

        public static async Task<List<PersonelAYRINTI>> ara(params Expression<Func<PersonelAYRINTI, bool>>[] kosullar)
       {
   return await veriTabani.PersonelAYRINTICizelgesi.ara(kosullar);
  }
        public static async Task<List<PersonelAYRINTI>> ara(veri.Varlik vari, params Expression<Func<PersonelAYRINTI, bool>>[] kosullar)
       {
   return await veriTabani.PersonelAYRINTICizelgesi.ara(vari,kosullar);
  }


    #region ozluk


public override string _cizelgeAdi()
 {
return "PersonelAYRINTI";   
 }


   public override string _turkceAdi() 
  {
    return "PersonelAYRINTI"; 
  } 
public override string _birincilAnahtarAdi()
 {
  return "personelKimlik"; 
}


    public override long _birincilAnahtar()
 {
     return this.personelKimlik;
}


    #endregion


  }
  }

