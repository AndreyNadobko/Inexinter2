using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Inex_inter.Domain;
using Inex_inter.Domain.Repositories.Abstract;
using Inex_inter.Domain.Repositories.EntityFramework;

using Inex_inter.Service;


namespace Inex_inter
{
    
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            //подключаем конфиг из appsetting.json
            Configuration.Bind("Project", new Config());

            //Подключаем нужный функционал приложения в качестве сервисов
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            //подключаем контекст БД
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

            //настраиваем identity систему 
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
             {
                 opts.User.RequireUniqueEmail = true;
                 opts.Password.RequiredLength = 6;
                 opts.Password.RequireNonAlphanumeric = false;
                 opts.Password.RequireLowercase = false;
                 opts.Password.RequireUppercase = false;
                 opts.Password.RequireDigit = false;
             }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //настраиваем authentication cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "inexInterAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;

            });

            //настраивам политику авторизации для Admin area
            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
            });

            //добавляем поддержку конроллеров и представлений (MVC)
            services.AddControllersWithViews(x =>
            {
                x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));


            })

             //выставляем совместимость с asp.net core 3.0
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //!!! порядок регистрации middleware очень важен
            
            // в процессе разработки важно видеть подробную информацию об ошибках
            if (env.IsDevelopment())
            
                app.UseDeveloperExceptionPage();
            
            //подключаем поддержку статичных файлов в приложении (css,js, и др)
            app.UseStaticFiles();

            //подключаем систему маршрутизации
            app.UseRouting();

            //подключаем аутентификацию и авторизацию
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            
            //Регистрация нужных маршрутов (ендпоинты)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Rooms}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Aboutus}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Contact}/{action=Index}/{id?}");









            });
        }
    }
}