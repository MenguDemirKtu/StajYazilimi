using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class YuklenenDosyalar : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int64 yuklenenDosyalarKimlik { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? dosyaKonumu { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ilgiliCizelgeAdi { get; set; }

        [Required]
        public Int64 ilgiliCizelgeKimlik { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? yuklemeTarihi { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string? dosyaUzantisi { get; set; }

        public Int32? i_dosyaTuruKimlik { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? kodu { get; set; }

    }
}
