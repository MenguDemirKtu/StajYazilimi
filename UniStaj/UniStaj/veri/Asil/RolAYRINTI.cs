using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 namespace UniStaj.veri
 {

 [Table("RolAYRINTI")]
    public partial class RolAYRINTI : Bilesen
 {
 [Key]
[Required]
 public   Int32 rolKimlik {get;set;}

[ Column(TypeName = "nvarchar(250)")]
 public   string  ? rolAdi {get;set;}

[ Column(TypeName = "nvarchar(500)")]
 public   string  ? tanitim {get;set;}

[Required]
 public   bool e_gecerlimi {get;set;}

[ Column(TypeName = "nvarchar(20)")]
 public   string  ? gecerlimi {get;set;}

[Required]
 public   bool varmi {get;set;}

 public   bool  ? e_varsayilanmi {get;set;}

 public   Int32  ? i_varsayilanOlduguKullaniciTuruKimlik {get;set;}

 public   bool  ? e_rolIslemiIcinmi {get;set;}

 public   Int32  ? i_rolIslemiKimlik {get;set;}

[ Column(TypeName = "nvarchar(150)")]
 public   string  ? kodu {get;set;}

 } 
 }
