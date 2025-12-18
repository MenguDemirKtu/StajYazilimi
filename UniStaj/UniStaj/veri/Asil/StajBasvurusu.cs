using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    public partial class StajBasvurusu : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Staj BaşvurusuKimlik")]
        [Required]
        public Int32 stajBasvurusukimlik { get; set; }

        [Display(Name = "Stajyer")]
        [Required]
        public Int32 i_stajyerKimlik { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

        [Display(Name = "Kurum Adı")]
        [Column(TypeName = "nvarchar(200)")]
        public string? stajKurumAdi { get; set; }

        [Display(Name = "Kurum Türü")]
        public Int32? i_stajkurumturukimlik { get; set; }

        [Display(Name = "Hizmet Alanı")]
        [Column(TypeName = "nvarchar(50)")]
        public string? hizmetAlani { get; set; }

        [Display(Name = "Vergi No")]
        [Column(TypeName = "nvarchar(50)")]
        public string? vergiNo { get; set; }

        [Display(Name = "IBAN")]
        [Column(TypeName = "nvarchar(30)")]
        public string? ibanNo { get; set; }

        [Display(Name = "Çalışan Sayısı")]
        public Int32? calisanSayisi { get; set; }

        [Display(Name = "Adresi")]
        [Column(TypeName = "nvarchar(150)")]
        public string? adresi { get; set; }

        [Display(Name = "Tel No")]
        [Column(TypeName = "nvarchar(12)")]
        public string? telNo { get; set; }

        [Display(Name = "E-Posta")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Display(Name = "Faks")]
        [Column(TypeName = "nvarchar(12)")]
        public string? faks { get; set; }

        [Display(Name = "Web Sitesi")]
        [Column(TypeName = "nvarchar(150)")]
        public string? webAdresi { get; set; }

        [Display(Name = "Yetkili Ad Soyad")]
        [Column(TypeName = "nvarchar(200)")]
        public string? yetkilisi { get; set; }

        [Display(Name = "Yetkili Görevi")]
        [Column(TypeName = "nvarchar(200)")]
        public string? yetkiliGorevi { get; set; }

        [Display(Name = "Yetkili Tel")]
        [Column(TypeName = "nvarchar(12)")]
        public string? yetkiliTel { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_stajTuruKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? baslangic { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? bitis { get; set; }

        [Display(Name = ".")]
        public Int32? gunSayisi { get; set; }

        [Display(Name = ".")]
        public Int32? sinifi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? calismaGunleri { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(40)")]
        public string? kodu { get; set; }

    }
}
