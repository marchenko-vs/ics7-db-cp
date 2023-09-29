using BlitzFlug.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace BlitzFlug
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public static string ConnectionString { get; set; }
        public static string CustomerData { get; set; }
        public static string ModeratorData { get; set; }
        public static string AdminData { get; set; }

        public Startup(IConfiguration configuration)
        { 
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) 
        {
            ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            CustomerData = Configuration.GetConnectionString("CustomerData");
            ModeratorData = Configuration.GetConnectionString("ModeratorData");
            AdminData = Configuration.GetConnectionString("AdminData");

            var moviesConfig = Configuration.GetValue<string>("MoviesServiceApiKey");

            services.AddControllersWithViews();
            services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option => {
                    option.LoginPath = "/Users/Register";
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                });

            Console.WriteLine(moviesConfig);
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment()) 
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Flights}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
