using Microsoft.AspNetCore.Authentication.Cookies;
using SanatGalerisi.Models;

namespace SanatGalerisi
{
	public class Program
	{
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add DbContext (veritabaný baðlamýnýzý eklediðinizden emin olun)
            builder.Services.AddDbContext<SanalSanatGalerisiDbContext>();

            // Add authentication services
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
                {
                    config.Cookie.Name = "UserLoginCookie";
                    config.LoginPath = "/Login/Login";
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    if (ctx.File.Name.EndsWith(".gz"))
                    {
                        ctx.Context.Response.Headers.Append("Content-Encoding", "gzip");
                        if (ctx.File.Name.EndsWith(".js.gz"))
                        {
                            ctx.Context.Response.ContentType = "application/javascript";
                        }
                        else if (ctx.File.Name.EndsWith(".wasm.gz"))
                        {
                            ctx.Context.Response.ContentType = "application/wasm";
                        }
                        else if (ctx.File.Name.EndsWith(".data.gz"))
                        {
                            ctx.Context.Response.ContentType = "application/octet-stream";
                        }
                    }
                }
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "unity",
                pattern: "{controller=Unity}/{action=Index}/{id?}");

            app.Run();
            }
        }
    }