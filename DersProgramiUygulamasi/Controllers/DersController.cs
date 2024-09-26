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
    public class DersController : Controller
    {
        private readonly DersProgramiService _dersProgramiService;

        public DersController(DersProgramiService dersProgramiService)
        {
            _dersProgramiService = dersProgramiService;
        }

        public IActionResult Index()
        {
            var dersler = _dersProgramiService.GetDersler();
            return View(dersler);
        }

        public IActionResult Create()
        {
            return View(new Ders());
        }

        [HttpPost]
        public IActionResult Create(Ders ders)
        {
            _dersProgramiService.DersEkle(ders);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var ders = _dersProgramiService.GetDersById(id);
            return View(ders);
        }

        [HttpPost]
        public IActionResult Edit(Ders ders)
        {
            if (ModelState.IsValid)
            {
                _dersProgramiService.DersGuncelle(ders);
                return RedirectToAction("Index");
            }
            return View(ders);
        }

        public IActionResult Delete(int id)
        {
            _dersProgramiService.DersSil(id);
            return RedirectToAction("Index");
        }
    }
}