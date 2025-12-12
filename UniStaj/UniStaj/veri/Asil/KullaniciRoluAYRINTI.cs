using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    [Table("KullaniciRoluAYRINTI")]
    public partial class KullaniciRoluAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int64 kullaniciRoluKimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_kullaniciKimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_rolKimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public bool e_gecerlimi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(250)")]
        public string? rolAdi { get; set; }

        [Display(Name = ".")]
        [Required, Column(TypeName = "nvarchar(50)")]
        public string kullaniciAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? gercekAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? ePostaAdresi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? unvan { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? ustTur { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? fotoBilgisi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(400)")]
        public string? ekAciklama { get; set; }

    }
}
