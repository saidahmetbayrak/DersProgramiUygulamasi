using DersProgramiUygulamasi.Models;
using DersProgramiUygulamasi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DersProgramiUygulamasi.Controllers
{
    public class HomeController : Controller
    {
        private readonly DersProgramiService _dersProgramiService;

        public HomeController(DersProgramiService dersProgramiService)
        {
            _dersProgramiService = dersProgramiService;
        }

        public IActionResult Index()
        {
            var dersProgrami = _dersProgramiService.GetHaftalikDersProgrami();
            return View(dersProgrami.GroupBy(dp => dp.Gun));
        }

        public IActionResult ProgramOlustur()
        {
            _dersProgramiService.HaftalikProgramOlustur();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
