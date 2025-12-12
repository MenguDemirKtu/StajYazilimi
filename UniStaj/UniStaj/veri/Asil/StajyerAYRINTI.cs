using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace UniStaj.veri
 {

 [Table("StajyerAYRINTI")]
    public partial class StajyerAYRINTI : Bilesen
 {
 [Key]
      [Display(Name = ".")] 
[Required]
 public   Int32 stajyerkimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(20)")]
 public   string  ? ogrenciNo {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(11)")]
 public   string  ? tcKimlikNo {get;set;}

      [Display(Name = ".")] 
[Required]
 public   Int32 i_stajBirimiKimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? stajBirimAdi {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? stajyerAdi {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? stajyerSoyadi {get;set;}

      [Display(Name = ".")] 
 public   Int32  ? sinifi {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(12)")]
 public   string  ? telefon {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(150)")]
 public   string  ? ePosta {get;set;}

      [Display(Name = ".")] 
 public   bool  ? varmi {get;set;}

 } 
 }
