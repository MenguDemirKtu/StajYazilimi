using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace UniStaj.veri
 {

 [Table("StajBirimiTurleriAYRINTI")]
    public partial class StajBirimiTurleriAYRINTI : Bilesen
 {
 [Key]
      [Display(Name = ".")] 
[Required]
 public   Int32 stajBirimiTurlerikimlik {get;set;}

      [Display(Name = ".")] 
[Required]
 public   Int32 i_stajBirimiKimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? stajBirimAdi {get;set;}

      [Display(Name = ".")] 
[Required]
 public   Int32 i_stajTuruKimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? stajTuruAdi {get;set;}

      [Display(Name = ".")] 
 public   Int32  ? gunu {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(20)")]
 public   string  ? siniflari {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(200)")]
 public   string  ? ekAciklama {get;set;}

      [Display(Name = ".")] 
 public   bool  ? varmi {get;set;}

 } 
 }
