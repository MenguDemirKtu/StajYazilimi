using System.Globalization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using UniStaj.Models;
using UniStaj.veri;

namespace UniStaj
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Genel.baglantiTumcesi = Configuration["ConnectionStrings:Mssql"] ?? "";
            services.AddDbContext<veri.Varlik>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:Mssql"]));

            services.AddDistributedMemoryCache();
            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddHttpContextAccessor();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(120);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.SameSite = SameSiteMode.Strict;
            });

            // Cookie Authentication
            services.AddAuthentication("Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
                    options.LoginPath = "/Giris/";
                    options.AccessDeniedPath = "/ErisimEngellendi/";
                    options.SlidingExpiration = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
            });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = long.MaxValue;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();

            Genel.devamGunuOlusturmaTarihi = DateTime.Today.AddDays(-1);

            try
            {
                Genel.sozcukler = new List<BYSSozcukAciklamaAYRINTI>();
                using (veri.Varlik vari = new Varlik())
                {
                    var liste = vari.YazilimAyaris.ToList();
                    bool guncellemeGerekiyormu = false;
                    foreach (var ayar in liste)
                    {
                        if (ayar.e_menuGuncellenecekmi == true || ayar.e_menuGuncellenecekmi == null)
                        {
                            guncellemeGerekiyormu = true;
                            YenileModel modeli = new YenileModel();
                            ayar.e_menuGuncellenecekmi = false;
                            veriTabani.YazilimAyariCizelgesi.kaydet(ayar, vari);
                        }
                    }

                    if (guncellemeGerekiyormu == true)
                    {
                        YenileModel model = new YenileModel();
                        model.yenile(vari);
                    }
                }
            }
            catch (Exception)
            {
                // Hatalar sessizce yutuluyor (log eklenmesi önerilir)
            }

            var supportedCultures = new[] { new CultureInfo("tr-TR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("tr-TR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseRouting();

            app.UseSession(); // ✔️ Session aktif hale getirildi
            app.UseAuthentication(); // ✔️ Authentication middleware EKLENDİ
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=AnaSayfa}/{action=Index}/{id?}");
            });
        }
    }
}
