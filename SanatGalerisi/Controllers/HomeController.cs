using Microsoft.AspNetCore.Mvc;
using SanatGalerisi.Models;
using System.Diagnostics;

namespace SanatGalerisi.Controllers
{
	public class HomeController : Controller
	{
        SanalSanatGalerisiDbContext c = new SanalSanatGalerisiDbContext();

  //      private readonly ILogger<HomeController> _logger;

		//public HomeController(ILogger<HomeController> logger)
		//{
		//	_logger = logger;
		//}

		public IActionResult Index()
		{
            var yazi = c.Anasayfas.ToList();
            return View(yazi);
        }

        public IActionResult Galeri()
		{
			var resimler = c.Galeris.ToList();
			return View(resimler);
		}

		public IActionResult Nedir()
		{
            var degerler = c.Nedirs.ToList();
            return View(degerler);
        }

        public IActionResult UnityScene()
        {
            return View();
        }
    }
}