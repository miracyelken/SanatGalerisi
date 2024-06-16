using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using SanatGalerisi.Models;
using System.Data;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace SanatGalerisi.Controllers
{
	public class AdminController : Controller
	{
        SanalSanatGalerisiDbContext c = new SanalSanatGalerisiDbContext();

        public ActionResult Index(string searchString)
        {
            var degerler = c.Galeris.ToList();
            return View(degerler);
        }

        [HttpGet]

        public ActionResult YeniGaleri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniGaleri(Galeri p)
        {
            c.Galeris.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GaleriSil(int Id)
        {
            var b = c.Galeris.Find(Id);
            c.Galeris.Remove(b);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GaleriGetir(int Id)
        {
            var bl = c.Galeris.Find(Id);
            return View("GaleriGetir", bl);
        }
        public ActionResult GaleriGuncelle(Galeri b)
        {
            var blg = c.Galeris.Find(b.Id);
            blg.Fotourl = b.Fotourl;
            blg.Aciklama = b.Aciklama;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult AdminYonetim()
        {
            var adminler = c.Admins.ToList();
            return View(adminler);
        }

        [Authorize]
        public ActionResult AdminSil(int Id)
        {
            var a = c.Admins.Find(Id);
            c.Admins.Remove(a);
            c.SaveChanges();
            return RedirectToAction("AdminYonetim");
        }

        [Authorize]
        public ActionResult AdminGetir(int Id)
        {
            var admn = c.Admins.Find(Id);
            return View("AdminGetir", admn);
        }
        [HttpGet]
        [Authorize]
        public ActionResult YeniAdmin()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult YeniAdmin(Admin v)
        {
            v.Sifre = HashHelper.CalculateSHA256Hash(v.Sifre);
            c.Admins.Add(v);
            c.SaveChanges();
            return RedirectToAction("AdminYonetim");
        }


        [Authorize]
        public ActionResult AdminGuncelle(Admin a)
        {
            var adme = c.Admins.Find(a.Id);
            adme.KullaniciAdi = a.KullaniciAdi;
            adme.Sifre = HashHelper.CalculateSHA256Hash(a.Sifre);
            c.SaveChanges();
            return RedirectToAction("AdminYonetim");
        }
    }
    public class HashHelper
    {
        public static string CalculateSHA256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

