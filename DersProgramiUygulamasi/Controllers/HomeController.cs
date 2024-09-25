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

        public async Task<IActionResult> Index()
        {
            var program = await _dersProgramiService.GetHaftalikDersProgramiAsync();
            var gunlereGoreProgram = program.GroupBy(p => p.Gun).OrderBy(g => g.Key).ToList();
            return View(gunlereGoreProgram);
        }

        public IActionResult ProgramOlustur()
        {
            _dersProgramiService.HaftalikProgramOlustur();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> OgretmenEkle(Ogretmen ogretmen)
        {
            await _dersProgramiService.AddOgretmenAsync(ogretmen);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DersEkle(Ders ders)
        {
            await _dersProgramiService.AddDersAsync(ders);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
