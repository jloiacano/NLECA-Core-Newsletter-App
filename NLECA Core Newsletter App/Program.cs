using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLECA_Core_Newsletter_App.Data;
using NLECA_Core_Newsletter_App.Data.Initializer;
using Serilog;
using System;

namespace NLECA_Core_Newsletter_App
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build();
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            //TODO - J - Delete below log reference (Be careful not to delete MigrateDatabase() function)
            Log.Error(
                new Exception("Test for Azure app settings issue before Migrate"), 
                string.Format("{0} {1}", Configuration["SuperAdminUser:UserName"], Configuration["ReadOnlyUser:UserName"]));

            MigrateDatabase(host);

            //TODO - J - Delete below log reference
            Log.Error(
                new Exception("Test for Azure app settings issue after Migrate"),
                string.Format("{0} {1}", Configuration["SuperAdminUser:UserName"], Configuration["ReadOnlyUser:UserName"]));

            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                try
                {
                    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    RoleAndAdminInitializer initializer = new RoleAndAdminInitializer(Configuration);
                    initializer.SeedData(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Seeding data in Main(); caused an exception");
                }
            }
            host.Run();
        }

        public static void MigrateDatabase(IHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseConfiguration(Configuration);
                    webBuilder.UseSerilog();
                });
    }
}
