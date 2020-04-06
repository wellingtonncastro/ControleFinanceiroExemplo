using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ControleFinanceiro.Models;
using ControleFinanceiro.Models.Email;
using ControleFinanceiro.Services.Interfaces;
using ControleFinanceiro.Services.Repository;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Rotativa.AspNetCore;

namespace ControleFinanceiro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ControleFinanceiroContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("ControleFinanceiroContext"),builder => builder.MigrationsAssembly("ControleFinanceiro")),ServiceLifetime.Singleton);


            services.AddIdentity<User, NivelAcesso>(options =>
             {
                 options.Password.RequireDigit = true;
                 options.Password.RequireLowercase = true;
                 options.Password.RequireNonAlphanumeric = true;
                 options.Password.RequireUppercase = true;
                 options.Password.RequiredLength = 8;
                 options.Password.RequiredUniqueChars = 1;

                 options.SignIn.RequireConfirmedEmail = true;
             }).AddDefaultUI().AddDefaultTokenProviders().AddEntityFrameworkStores<ControleFinanceiroContext>();
            
            services.Configure<ConfiguracaoEmail>(Configuration.GetSection("ConfiguracaoEmail"));
            services.AddScoped<IEmail, Email>();

            services.AddScoped<SeedingServices>();

            services.AddScoped<IYieldRepository,YieldRepository>();
            services.AddScoped<IExpenseRepository,ExpenseRepository>();
            services.AddScoped<IAnothersExpenseRepository,AnothersExpenseRepository>();
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSession();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                options.User.AllowedUserNameCharacters =
               " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%¨&*()_+}{`^~?/:><,.''=ºª ";
            });

            services.ConfigureApplicationCookie(opcoes =>
            {
                opcoes.Cookie.HttpOnly = true;
                opcoes.ExpireTimeSpan = TimeSpan.FromMinutes(50);
                opcoes.LoginPath = "/Users/Login";
                opcoes.SlidingExpiration = true;
            });

        }

        [Obsolete]
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingServices seeding)
        {
            var enUS = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS }
            };

            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                seeding.Seed();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IHostingEnvironment)env, "Rotativa");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Users}/{action=Login}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
