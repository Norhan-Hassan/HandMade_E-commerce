using HandMade.DataAccess.Data;
using HandMade.DataAccess.Repo_Implementations;
using HandMade.Entities.Models;
using HandMade.Entities.Repo_Interfaces;
using HandMade.Web.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace HandMade.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );
            //stripe
            builder.Services.Configure<StripeInfo>(
                builder.Configuration.GetSection("stripe")
                );

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(

               // options=>options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromHours(2)

                ).AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddHttpContextAccessor();


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            
            StripeConfiguration.ApiKey = builder.Configuration.GetSection("stripe:Secretkey").Get<string>();


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "Identity",
                pattern: "{area=Identity}/{controller=Account}/{action=Register}/{id?}"
                );

            app.MapControllerRoute(
                name: "User",
                pattern: "{area=User}/{controller=Home}/{action=Details}/{id?}"
                );

               app.MapControllerRoute(
                name: "Admin",
                pattern: "{area=Admin}/{controller=Product}/{action=Index}/{id?}"
              );
            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}
