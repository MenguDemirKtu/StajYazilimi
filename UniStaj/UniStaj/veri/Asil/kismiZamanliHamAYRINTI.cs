using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("kismiZamanliHamAYRINTI")]
    public partial class kismiZamanliHamAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 id { get; set; }

        public Int32? ogrenciNo { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? kisiselBilgi { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? bursAliyormu { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? ikametBilgisi { get; set; }

        public Int32? kiraBedeli { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? aileBilgisi { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? aileSosyal { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? aileGelirDuzeyi { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? aileBarinmaSekli { get; set; }

        public double? aileKiraBedeli { get; set; }

        public double? aileToplamGelir { get; set; }

        public Int32? aileBireySayisi { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? baskaOkuyanCocuk { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? istenenBirim { get; set; }

        public bool? varmi { get; set; }

        public double? deger { get; set; }

        public double? fitnes { get; set; }

        public double? hasanpolat { get; set; }

        public double? kulupler { get; set; }

        public double? sahil { get; set; }

        [Column(TypeName = "nvarchar(201)")]
        public string? ogrenciKimligi { get; set; }

    }
}
