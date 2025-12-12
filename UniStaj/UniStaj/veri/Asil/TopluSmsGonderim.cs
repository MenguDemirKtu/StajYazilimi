using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class TopluSmsGonderim : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 topluSMSGonderimKimlik { get; set; }

        [Display(Name = "Metin")]
        [Column(TypeName = "nvarchar(250)")]
        public string? metin { get; set; }

        [Display(Name = "Tarih")]
        [Column(TypeName = "datetime")]
        public DateTime? tarih { get; set; }

        [Display(Name = "Açıklama")]
        [Column(TypeName = "nvarchar(1000)")]
        public string? aciklama { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = "Kodu")]
        [Column(TypeName = "nvarchar(150)")]
        public string? kodu { get; set; }

        [Display(Name = "SMS Atılacak mı")]
        public bool? e_smsGonderilecekmi { get; set; }

        [Display(Name = "E-Posta Gönerilecek mi")]
        public bool? e_epostaGonderilecekmi { get; set; }

        [Display(Name = "E-Posta Metin")]
        [Column(TypeName = "text")]
        public string? ePostaMetin { get; set; }

        [Display(Name = "Gönderilen SMS")]
        public Int32? gonderilenSMSSayisi { get; set; }

        [Display(Name = "Gönderilen E-Posta")]
        public Int32? gonderilenEPostaSayisi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePostaKonusu { get; set; }

    }
}
