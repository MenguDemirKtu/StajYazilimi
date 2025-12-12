using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace UniStaj.veri
 {

 [Table("StajBirimYetkilisiBirimiAYRINTI")]
    public partial class StajBirimYetkilisiBirimiAYRINTI : Bilesen
 {
 [Key]
      [Display(Name = ".")] 
[Required]
 public   Int32 stajBirimYetkilisiBirimikimlik {get;set;}

      [Display(Name = ".")] 
 public   Int32  ? i_stajBirimYetkilisiKimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(11)")]
 public   string  ? tcKimlikNo {get;set;}

      [Display(Name = ".")] 
 public   Int32  ? i_stajBirimiKimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? stajBirimAdi {get;set;}

      [Display(Name = ".")] 
 public   bool  ? e_gecerliMi {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(20)")]
 public   string  ? gecerliMi {get;set;}

      [Display(Name = ".")] 
 public   bool  ? varmi {get;set;}

 } 
 }
