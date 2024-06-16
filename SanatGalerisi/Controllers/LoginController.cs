using Microsoft.AspNetCore.Mvc;
using SanatGalerisi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace SanatGalerisi.Controllers
{
    public class LoginController : Controller
    {
        SanalSanatGalerisiDbContext c = new SanalSanatGalerisiDbContext();

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Admin ad)
        {
            var hashedPassword = HashHelper.CalculateSHA256Hash(ad.Sifre);
            var bilgiler = c.Admins.FirstOrDefault(x => x.KullaniciAdi == ad.KullaniciAdi && x.Sifre == hashedPassword);

            if (bilgiler != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, bilgiler.KullaniciAdi)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("YeniAdmin", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

        public IActionResult UserLogin()
        {
            return View();
        }
    }
}

