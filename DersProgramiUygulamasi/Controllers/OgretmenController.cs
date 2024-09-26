using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DersProgramiUygulamasi.Models;
using DersProgramiUygulamasi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DersProgramiUygulamasi.Controllers
{
    public class OgretmenController : Controller
    {
        private readonly DersProgramiService _dersProgramiService;

        public OgretmenController(DersProgramiService dersProgramiService)
        {
            _dersProgramiService = dersProgramiService;
        }

        public IActionResult Index()
        {
            var ogretmenler = _dersProgramiService.GetOgretmenler();
            return View(ogretmenler);
        }

        public IActionResult Create()
        {
            return View(new Ogretmen());
        }

        [HttpPost]
        public IActionResult Create(Ogretmen ogretmen)
        {
            _dersProgramiService.OgretmenEkle(ogretmen);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var ogretmen = _dersProgramiService.GetOgretmenById(id);
            return View(ogretmen);
        }

        [HttpPost]
        public IActionResult Edit(Ogretmen ogretmen)
        {
            if (ModelState.IsValid)
            {
                _dersProgramiService.OgretmenGuncelle(ogretmen);
                return RedirectToAction("Index");
            }
            return View(ogretmen);
        }

        public IActionResult Delete(int id)
        {
            _dersProgramiService.OgretmenSil(id);
            return RedirectToAction("Index");
        }
    }
}