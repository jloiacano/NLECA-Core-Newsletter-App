using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Data;
using NLECA_Core_Newsletter_App.Data.Initializer;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using NLECA_Core_Newsletter_App.Service.Services;
using Serilog;
using System;
using System.Security.Claims;

namespace NLECA_Core_Newsletter_App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")));
            }
            catch (Exception ex)
            {
                Log.Error("Error adding database context in Startup.cs", ex);
            }

            try
            {
                services.AddDefaultIdentity<ApplicationIdentityUser>()
                    .AddRoles<ApplicationRole>()
                    .AddUserManager<ApplicationIdentityUserManager>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

                services.AddScoped<IUserClaimsPrincipalFactory<ApplicationIdentityUser>, NLECAUserClaimsPrincipalFactory>();
            }
            catch (Exception ex)
            {
                Log.Error("Error initializing IdentityFramework in Startup.cs", ex);
            }

            try
            {
                services.AddDistributedMemoryCache();
            }
            catch (Exception ex)
            {
                Log.Error("Error adding distributed memory cache in Startup.cs", ex);
            }


            try
            {
                services.AddSession(options =>
                {
                    options.IdleTimeout = TimeSpan.FromSeconds(3600); // reset after an hour
                    options.Cookie.HttpOnly = false;
                    options.Cookie.IsEssential = false;
                });
            }
            catch (Exception ex)
            {
                Log.Error("Error adding sesion options in Startup.cs", ex);
            }

            try
            {
                services.AddControllersWithViews();
                services.AddRazorPages();
            }
            catch (Exception ex)
            {
                Log.Error("Error adding Core services in Startup.cs", ex);
            }


            try
            {
                services.AddAuthentication()
                    .AddFacebook(facebookOptions =>
                    {
                        IConfigurationSection facebookAuthSection =
                            Configuration.GetSection("Authentication:Facebook");

                        facebookOptions.AppId = facebookAuthSection["AppId"];
                        facebookOptions.AppSecret = facebookAuthSection["AppSecret"];
                    });
            }
            catch (Exception ex)
            {
                Log.Error("Error adding Facebook Authentication in Startup.cs", ex);
            }


            try
            {
                services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        IConfigurationSection googleAuthNSection =
                            Configuration.GetSection("Authentication:Google");

                        options.ClientId = googleAuthNSection["ClientId"];
                        options.ClientSecret = googleAuthNSection["ClientSecret"];
                    });
            }
            catch (Exception ex)
            {
                Log.Error("Error adding Google Authentication in Startup.cs", ex);
            }

            // TODO - J - once up and running and have TOS page etc, go to https://developer.twitter.com/
            // and set up Permissions to get email and users name, and then re-enable below code.
            //try
            //{
            //    services.AddAuthentication()
            //        .AddTwitter(twitterOptions =>
            //        {
            //            IConfigurationSection twitterAuthSection =
            //                Configuration.GetSection("Authentication:Twitter");

            //            twitterOptions.ConsumerKey = twitterAuthSection["ConsumerAPIKey"];
            //            twitterOptions.ConsumerSecret = twitterAuthSection["ConsumerSecret"];
            //            twitterOptions.RetrieveUserDetails = true;
            //        });
            //}
            //catch (Exception ex)
            //{
            //    Log.Error("Error adding Twitter Authentication in Startup.cs", ex);
            //}


            try
            {
                services.AddScoped<IHolidayService, HolidayService>();
                services.AddScoped<ISplashScreenService, SplashScreenService>();
                services.AddScoped<INewsletterService, NewsletterService>();
                services.AddScoped<IArticleService, ArticleService>();
                services.AddScoped<ISQLHelperService, SQLHelperService>();
                services.AddScoped<IImageService, ImageService>();
            }
            catch (Exception ex)
            {
                Log.Error("Error injecting service interfaces in Startup.cs", ex);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IHost host)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            loggerFactory.AddSerilog();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            // Seeding database roles and essential users here to have access to Azure configured App Settings
            using IServiceScope scope = host.Services.CreateScope();
            IServiceProvider serviceProvider = scope.ServiceProvider;
            try
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationIdentityUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                RoleAndAdminInitializer initializer = new RoleAndAdminInitializer(Configuration);
                initializer.SeedData(userManager, roleManager);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Seeding of the database failed in Startup.cs Configure()");
            }
        }
    }
}
