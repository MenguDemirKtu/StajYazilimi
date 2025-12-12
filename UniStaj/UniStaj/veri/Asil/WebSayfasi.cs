using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class WebSayfasi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 webSayfasiKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? hamAdresi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? sayfaBasligi { get; set; }

        [Required]
        public Int32 i_modulKimlik { get; set; }

        [Required]
        public Int32 i_sayfaTuruKimlik { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Column(TypeName = "text")]
        public string? aciklama { get; set; }

        public bool? varmi { get; set; }

        [Required]
        public Int64 i_fotoKimlik { get; set; }

        public bool? e_izinSayfasindaGorunsunmu { get; set; }

        public bool? e_varsayilanEklemeAcikmi { get; set; }

        public bool? e_varsayilanGorunmeAcikmi { get; set; }

        public bool? e_varsayilanGuncellemeAcikmi { get; set; }

        public bool? e_varsayilanSilmeAcikmi { get; set; }

        [Column(TypeName = "nvarchar(1500)")]
        public string? dokumAciklamasi { get; set; }

        [Column(TypeName = "nvarchar(1500)")]
        public string? kartAciklamasi { get; set; }

    }
}
