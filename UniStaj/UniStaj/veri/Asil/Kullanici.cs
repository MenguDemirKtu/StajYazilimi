using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Kullanici : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 kullaniciKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string kullaniciAdi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? sifre { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? guvenlikSorusu { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? guvenlikYaniti { get; set; }

        public Int32? i_kullaniciTuruKimlik { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? gercekAdi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ePostaAdresi { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? unvan { get; set; }

        public bool? varmi { get; set; }

        public bool? e_rolTabanlimi { get; set; }

        public Int64? i_fotoKimlik { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dogumTarihi { get; set; }

        public Int32? i_dilKimlik { get; set; }

        public bool? e_faalmi { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string? ekAciklama { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? kodu { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? sicilNo { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? ogrenciNo { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? ustTur { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? fotoBilgisi { get; set; }

        public Int32? rolSayisi { get; set; }

        public Int64? y_kisiKimlik { get; set; }

        public Int32? y_iskurOgrencisiKimlik { get; set; }

        public Int32? y_personelKimlik { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? bysyeIlkGirisTarihi { get; set; }

        public bool? e_sifreDegisecekmi { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? sonSifreDegistirmeTarihi { get; set; }

        public bool? e_sozlesmeOnaylandimi { get; set; }

        [Column(TypeName = "nvarchar(8)")]
        public string? ciftOnayKodu { get; set; }

        public Int32? sonGunGirisi { get; set; }

    }
}
