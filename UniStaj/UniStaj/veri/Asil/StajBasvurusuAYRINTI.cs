using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    [Table("StajBasvurusuAYRINTI")]
    public partial class StajBasvurusuAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 stajBasvurusukimlik { get; set; }

        [Required]
        public Int32 i_stajyerKimlik { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? ogrenciNo { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Required]
        public Int32 i_stajBirimiKimlik { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? stajyerAdi { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? stajyerSoyadi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? stajKurumAdi { get; set; }

        public Int32? i_stajkurumturukimlik { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? hizmetAlani { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? vergiNo { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string? ibanNo { get; set; }

        public Int32? calisanSayisi { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? adresi { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string? telNo { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string? faks { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? webAdresi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? yetkilisi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? yetkiliGorevi { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string? yetkiliTel { get; set; }

        [Required]
        public Int32 i_stajTuruKimlik { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? baslangic { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? bitis { get; set; }

        public Int32? gunSayisi { get; set; }

        public Int32? sinifi { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? calismaGunleri { get; set; }

        [Column(TypeName = "nvarchar(40)")]
        public string? kodu { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? stajTuruAdi { get; set; }

        public Int32? i_stajBasvuruDurumuKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string StajBasvuruDurumuAdi { get; set; }

    }
}
