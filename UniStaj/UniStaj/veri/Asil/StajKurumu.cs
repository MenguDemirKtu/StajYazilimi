using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace UniStaj.veri
 {

    public partial class StajKurumu : Bilesen
 {
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
 [Key]
      [Display(Name = "Staj KurumuKimlik")] 
[Required]
 public   Int32 stajKurumukimlik {get;set;}

      [Display(Name = "Staj Kurum Adı")] 
[ Column(TypeName = "nvarchar(200)")]
 public   string  ? stajKurumAdi {get;set;}

      [Display(Name = "stajKurumTuru")] 
[Required]
 public   Int32 i_stajkurumturukimlik {get;set;}

      [Display(Name = "Hizmet Alanı")] 
[ Column(TypeName = "nvarchar(50)")]
 public   string  ? hizmetAlani {get;set;}

      [Display(Name = "Web Adresi")] 
[ Column(TypeName = "nvarchar(150)")]
 public   string  ? webAdresi {get;set;}

      [Display(Name = "Vergi No")] 
[ Column(TypeName = "nvarchar(50)")]
 public   string  ? vergiNo {get;set;}

      [Display(Name = "IBAN No")] 
[ Column(TypeName = "nvarchar(30)")]
 public   string  ? ibanNo {get;set;}

      [Display(Name = "Çalışan Sayısı")] 
 public   Int32  ? calisanSayisi {get;set;}

      [Display(Name = "Adresi")] 
[ Column(TypeName = "nvarchar(150)")]
 public   string  ? adresi {get;set;}

      [Display(Name = "Tel No")] 
[ Column(TypeName = "nvarchar(12)")]
 public   string  ? telNo {get;set;}

      [Display(Name = "E Posta")] 
[ Column(TypeName = "nvarchar(150)")]
 public   string  ? ePosta {get;set;}

      [Display(Name = "Faks")] 
[ Column(TypeName = "nvarchar(12)")]
 public   string  ? faks {get;set;}

      [Display(Name = "Kara Listede mi")] 
 public   bool  ? e_karaListedeMi {get;set;}

      [Display(Name = "varmi")] 
 public   bool  ? varmi {get;set;}

 } 
 }
