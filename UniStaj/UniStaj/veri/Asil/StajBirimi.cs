using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace UniStaj.veri
 {

    public partial class StajBirimi : Bilesen
 {
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
 [Key]
      [Display(Name = ".")] 
[Required]
 public   Int32 stajBirimikimlik {get;set;}

      [Display(Name = "Staj Birim Adı")] 
[ Column(TypeName = "nvarchar(100)")]
 public   string  ? stajBirimAdi {get;set;}

      [Display(Name = "Telefon")] 
[ Column(TypeName = "nvarchar(11)")]
 public   string  ? telefon {get;set;}

      [Display(Name = "E-Posta")] 
[ Column(TypeName = "nvarchar(150)")]
 public   string  ? ePosta {get;set;}

      [Display(Name = "Birim Sorumlu Adı")] 
[ Column(TypeName = "nvarchar(50)")]
 public   string  ? birimSorumluAdi {get;set;}

      [Display(Name = "Alt Birim")] 
 public   bool  ? e_altBirimMi {get;set;}

      [Display(Name = "Üst Staj Birimi")] 
 public   Int32  ? i_ustStajBirimiKimlik {get;set;}

      [Display(Name = ".")] 
 public   bool  ? varmi {get;set;}

 } 
 }
