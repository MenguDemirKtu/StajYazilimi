using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace UniStaj.veri
 {

 [Table("StajTuruAYRINTI")]
    public partial class StajTuruAYRINTI : Bilesen
 {
 [Key]
      [Display(Name = ".")] 
[Required]
 public   Int32 stajTurukimlik {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? stajTuruAdi {get;set;}

      [Display(Name = ".")] 
[ Column(TypeName = "nvarchar(400)")]
 public   string  ? tanim {get;set;}

      [Display(Name = ".")] 
 public   bool  ? varmi {get;set;}

 } 
 }
