using Microsoft.EntityFrameworkCore; //
namespace UniStaj.veri
{
public class Varlik : DbContext
{
public Varlik()
{
}
public void ConfigureServices(IServiceCollection services)
{
services.AddDbContext<Varlik>(options => options.UseSqlServer(Genel.baglantiTumcesi));
}
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
optionsBuilder.UseSqlServer(Genel.baglantiTumcesi);
}
public Varlik(DbContextOptions<Varlik> options) : base(options)
{
var nedir = options;
}
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
foreach (var entity in modelBuilder.Model.GetEntityTypes())
{
string ad = entity.Name.Substring(0, entity.Name.Length);
int yer = ad.LastIndexOf(".");
if (yer != -1)
ad = ad.Substring(yer);
ad = ad.Replace(".", "").Trim();
modelBuilder.Entity(entity.Name).ToTable(ad);
}
}
#region ogeler
public virtual DbSet<ResimAyari> ResimAyaris { get; set; }
public virtual DbSet<AramaTalebi> AramaTalebis { get; set; }
public virtual DbSet<AramaTalebiAYRINTI> AramaTalebiAYRINTIs { get; set; }
public virtual DbSet<BelgeTuru> BelgeTurus { get; set; }
public virtual DbSet<BelgeTuruAYRINTI> BelgeTuruAYRINTIs { get; set; }
public virtual DbSet<BysMenu> BysMenus { get; set; }
public virtual DbSet<BysMenuAYRINTI> BysMenuAYRINTIs { get; set; }
public virtual DbSet<BYSSozcuk> BYSSozcuks { get; set; }
public virtual DbSet<BYSSozcukAciklama> BYSSozcukAciklamas { get; set; }
public virtual DbSet<BYSSozcukAciklamaAYRINTI> BYSSozcukAciklamaAYRINTIs { get; set; }
public virtual DbSet<BYSSozcukAYRINTI> BYSSozcukAYRINTIs { get; set; }
public virtual DbSet<Duyuru> Duyurus { get; set; }
public virtual DbSet<GaleriFotosu> GaleriFotosus { get; set; }
public virtual DbSet<Galeri> Galeris { get; set; }
public virtual DbSet<GaleriAYRINTI> GaleriAYRINTIs { get; set; }
public virtual DbSet<GaleriFotosuAYRINTI> GaleriFotosuAYRINTIs { get; set; }
public virtual DbSet<DuyuruAYRINTI> DuyuruAYRINTIs { get; set; }
public virtual DbSet<DuyuruRolBagi> DuyuruRolBagis { get; set; }
public virtual DbSet<DuyuruRolBagiAYRINTI> DuyuruRolBagiAYRINTIs { get; set; }
public virtual DbSet<EPostaKalibi> EPostaKalibis { get; set; }
public virtual DbSet<EPostaKalibiAYRINTI> EPostaKalibiAYRINTIs { get; set; }
public virtual DbSet<FederasyonBildirimGondermeAyari> FederasyonBildirimGondermeAyaris { get; set; }
public virtual DbSet<FederasyonBildirimGondermeAyariAYRINTI> FederasyonBildirimGondermeAyariAYRINTIs { get; set; }
public virtual DbSet<Fotograf> Fotografs { get; set; }
public virtual DbSet<FotografAYRINTI> FotografAYRINTIs { get; set; }
public virtual DbSet<GonderilenEPosta> GonderilenEPostas { get; set; }
public virtual DbSet<GonderilenEPostaAYRINTI> GonderilenEPostaAYRINTIs { get; set; }
public virtual DbSet<GonderilenSMS> GonderilenSMSs { get; set; }
public virtual DbSet<GonderilenSMSAYRINTI> GonderilenSMSAYRINTIs { get; set; }
public virtual DbSet<GunlukGuvenlikAnahtari> GunlukGuvenlikAnahtaris { get; set; }
public virtual DbSet<Hata> hatas { get; set; }
public virtual DbSet<HataBildirimi> HataBildirimis { get; set; }
public virtual DbSet<HataBildirimiAYRINTI> HataBildirimiAYRINTIs { get; set; }
public virtual DbSet<HataYazismasi> HataYazismasis { get; set; }
public virtual DbSet<HataYazismasiAYRINTI> HataYazismasiAYRINTIs { get; set; }
public virtual DbSet<Ilce> Ilces { get; set; }
public virtual DbSet<IlceAYRINTI> IlceAYRINTIs { get; set; }
public virtual DbSet<Kayit> Kayits { get; set; }
public virtual DbSet<KayitAYRINTI> KayitAYRINTIs { get; set; }
public virtual DbSet<Kisi> Kisis { get; set; }
public virtual DbSet<KisiAYRINTI> KisiAYRINTIs { get; set; }
public virtual DbSet<Kullanici> Kullanicis { get; set; }
public virtual DbSet<KullaniciAYRINTI> KullaniciAYRINTIs { get; set; }
public virtual DbSet<KullaniciBildirimi> KullaniciBildirimis { get; set; }
public virtual DbSet<KullaniciBildirimiAYRINTI> KullaniciBildirimiAYRINTIs { get; set; }
public virtual DbSet<KullaniciRolu> KullaniciRolus { get; set; }
public virtual DbSet<KullaniciRoluAYRINTI> KullaniciRoluAYRINTIs { get; set; }
public virtual DbSet<KullaniciRoluIzinAYRINTI> KullaniciRoluIzinAYRINTIs { get; set; }
public virtual DbSet<KullaniciRolWebSayfasiIzniAYRINTI> KullaniciRolWebSayfasiIzniAYRINTIs { get; set; }
public virtual DbSet<KullaniciWebSayfasiIzni> KullaniciWebSayfasiIznis { get; set; }
public virtual DbSet<KullaniciWebSayfasiIzniAYRINTI> KullaniciWebSayfasiIzniAYRINTIs { get; set; }
public virtual DbSet<Modul> Moduls { get; set; }
public virtual DbSet<ModulAYRINTI> ModulAYRINTIs { get; set; }
public virtual DbSet<OturumAcma> OturumAcmas { get; set; }
public virtual DbSet<OturumAcmaAYRINTI> OturumAcmaAYRINTIs { get; set; }
public virtual DbSet<ref_Cinsiyet> ref_Cinsiyets { get; set; }
public virtual DbSet<ref_Dil> ref_Dils { get; set; }
public virtual DbSet<ref_EPostaTuru> ref_EPostaTurus { get; set; }
public virtual DbSet<ref_FederasyonBildirimTuru> ref_FederasyonBildirimTurus { get; set; }
public virtual DbSet<ref_KisiTuru> ref_KisiTurus { get; set; }
public virtual DbSet<ref_KullaniciTuru> ref_KullaniciTurus { get; set; }
public virtual DbSet<ref_OturumAcmaTuru> ref_OturumAcmaTurus { get; set; }
public virtual DbSet<ref_RolIslemi> ref_RolIslemis { get; set; }
public virtual DbSet<ref_sayfaTuru> ref_sayfaTurus { get; set; }
public virtual DbSet<ref_SmsGonderimSitesi> ref_SmsGonderimSitesis { get; set; }
public virtual DbSet<ref_SmsGonderimTuru> ref_SmsGonderimTurus { get; set; }
public virtual DbSet<ref_SozlesmeTuru> ref_SozlesmeTurus { get; set; }
public virtual DbSet<Rol> Rols { get; set; }
public virtual DbSet<RolAYRINTI> RolAYRINTIs { get; set; }
public virtual DbSet<RolWebSayfasiIzni> RolWebSayfasiIznis { get; set; }
public virtual DbSet<RolWebSayfasiIzniAYRINTI> RolWebSayfasiIzniAYRINTIs { get; set; }
public virtual DbSet<SayfaDegisimi> SayfaDegisimis { get; set; }
public virtual DbSet<SayfaDegisimiAYRINTI> SayfaDegisimiAYRINTIs { get; set; }
public virtual DbSet<Sehir> Sehirs { get; set; }
public virtual DbSet<SehirAYRINTI> SehirAYRINTIs { get; set; }
public virtual DbSet<SifreDegisikligi> SifreDegisikligis { get; set; }
public virtual DbSet<SifreDegisikligiAYRINTI> SifreDegisikligiAYRINTIs { get; set; }
public virtual DbSet<SifremiUnuttum> SifremiUnuttums { get; set; }
public virtual DbSet<SifremiUnuttumAYRINTI> SifremiUnuttumAYRINTIs { get; set; }
public virtual DbSet<SistemHatasi> SistemHatasis { get; set; }
public virtual DbSet<SistemHatasiAYRINTI> SistemHatasiAYRINTIs { get; set; }
public virtual DbSet<SmsDosyasi> SmsDosyasis { get; set; }
public virtual DbSet<SmsDosyasiAYRINTI> SmsDosyasiAYRINTIs { get; set; }
public virtual DbSet<SmsKalibi> SmsKalibis { get; set; }
public virtual DbSet<SmsKalibiAYRINTI> SmsKalibiAYRINTIs { get; set; }
public virtual DbSet<Sozlesme> Sozlesmes { get; set; }
public virtual DbSet<SozlesmeAYRINTI> SozlesmeAYRINTIs { get; set; }
public virtual DbSet<SozlesmeOnayi> SozlesmeOnayis { get; set; }
public virtual DbSet<TopluEPostaAlicisi> TopluEPostaAlicisis { get; set; }
public virtual DbSet<TopluEPostaAlicisiAYRINTI> TopluEPostaAlicisiAYRINTIs { get; set; }
public virtual DbSet<TopluSMSAlicisi> TopluSMSAlicisis { get; set; }
public virtual DbSet<TopluSMSAlicisiAYRINTI> TopluSMSAlicisiAYRINTIs { get; set; }
public virtual DbSet<TopluSmsGonderim> TopluSmsGonderims { get; set; }
public virtual DbSet<TopluSmsGonderimAYRINTI> TopluSmsGonderimAYRINTIs { get; set; }
public virtual DbSet<Ulke> Ulkes { get; set; }
public virtual DbSet<UlkeAYRINTI> UlkeAYRINTIs { get; set; }
public virtual DbSet<WebSayfasi> WebSayfasis { get; set; }
public virtual DbSet<WebSayfasiAYRINTI> WebSayfasiAYRINTIs { get; set; }
public virtual DbSet<YazilimAyari> YazilimAyaris { get; set; }
public virtual DbSet<YazilimAyariAYRINTI> YazilimAyariAYRINTIs { get; set; }
public virtual DbSet<YazilimGuvenligi> YazilimGuvenligis { get; set; }
public virtual DbSet<YazilimGuvenligiAYRINTI> YazilimGuvenligiAYRINTIs { get; set; }
public virtual DbSet<YuklenenDosyalar> YuklenenDosyalars { get; set; }
public virtual DbSet<ResimAyariAYRINTI> ResimAyariAYRINTIs { get; set; }
public virtual DbSet<SosyalMedyaAYRINTI> SosyalMedyaAYRINTIs { get; set; }
public virtual DbSet<SosyalMedya> SosyalMedyas { get; set; }
public virtual DbSet<SimgeAYRINTI> SimgeAYRINTIs { get; set; }
public virtual DbSet<Simge> Simges { get; set; }
public virtual DbSet<StajTuruAYRINTI> StajTuruAYRINTIs { get; set; }
public virtual DbSet<StajTuru> StajTurus { get; set; }
public virtual DbSet<StajBirimiTurleriAYRINTI> StajBirimiTurleriAYRINTIs { get; set; }
public virtual DbSet<StajBirimiAYRINTI> StajBirimiAYRINTIs { get; set; }
public virtual DbSet<StajBirimiTurleri> StajBirimiTurleris { get; set; }
public virtual DbSet<StajBirimi> StajBirimis { get; set; }
public virtual DbSet<ref_StajTipi> ref_StajTipis { get; set; }
public virtual DbSet<StajyerAYRINTI> StajyerAYRINTIs { get; set; }
public virtual DbSet<Stajyer> Stajyers { get; set; }
public virtual DbSet<StajKurumTuruAYRINTI> StajKurumTuruAYRINTIs { get; set; }
public virtual DbSet<StajKurumTuru> StajKurumTurus { get; set; }
public virtual DbSet<StajKurumuAYRINTI> StajKurumuAYRINTIs { get; set; }
public virtual DbSet<StajKurumu> StajKurumus { get; set; }
public virtual DbSet<YuklenenDosyalarAYRINTI> YuklenenDosyalarAYRINTIs { get; set; }
#endregion
    }
}
