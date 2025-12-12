using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Rol : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 rolKimlik { get; set; }

        [Display(Name = "Rol Adı")]
        [Column(TypeName = "nvarchar(250)")]
        public string? rolAdi { get; set; }

        [Display(Name = "Tanıtım")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Display(Name = "Geçerli mi")]
        [Required]
        public bool e_gecerlimi { get; set; }

        [Display(Name = ".")]
        [Required]
        public bool varmi { get; set; }

        [Display(Name = "Varsayılan mı")]
        public bool? e_varsayilanmi { get; set; }

        [Display(Name = "Kullanıcı Türü ")]
        public Int32? i_varsayilanOlduguKullaniciTuruKimlik { get; set; }

        [Display(Name = "Rol İşlemi İçin mi")]
        public bool? e_rolIslemiIcinmi { get; set; }

        [Display(Name = "Rol İşlemi")]
        public Int32? i_rolIslemiKimlik { get; set; }

        [Display(Name = "Kodu")]
        [Column(TypeName = "nvarchar(150)")]
        public string? kodu { get; set; }

    }
}
