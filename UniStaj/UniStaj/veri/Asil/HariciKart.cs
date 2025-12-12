using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class HariciKart : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Harici KartKimlik")]
        [Required]
        public Int32 hariciKartkimlik { get; set; }

        [Display(Name = "TC Kimlik No")]
        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Display(Name = "Ad")]
        [Column(TypeName = "nvarchar(100)")]
        public string? ad { get; set; }

        [Display(Name = "Soyad")]
        [Column(TypeName = "nvarchar(100)")]
        public string? soyad { get; set; }

        [Display(Name = "Telefon")]
        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Display(Name = "E Posta Adresi")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePostaAdresi { get; set; }

        [Display(Name = "Doğum Tarihi")]
        [Column(TypeName = "datetime")]
        public DateTime? dogumTarihi { get; set; }

        [Display(Name = "Kart Birimi")]
        [Required]
        public Int32 i_kartBirimiKimlik { get; set; }

        [Display(Name = "Unvan")]
        [Column(TypeName = "nvarchar(150)")]
        public string? unvan { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Şifre")]
        [Column(TypeName = "nvarchar(256)")]
        public string? sifre { get; set; }

        [Display(Name = "Meslek")]
        [Column(TypeName = "nvarchar(200)")]
        public string? meslek { get; set; }

        [Display(Name = "Verilme Amacı")]
        [Column(TypeName = "nvarchar(500)")]
        public string? verilmeAmaci { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
