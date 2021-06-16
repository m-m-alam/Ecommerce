using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagController : Controller
    {
        private ApplicationDbContext _context;
        public SpecialTagController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.SpecialTags.ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SpecialTag model)
        {
            if (ModelState.IsValid)
            {
                _context.SpecialTags.Add(model);
                _context.SaveChanges();
                TempData["Save"] = " Tag Save SuccessFully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var data = _context.SpecialTags.Find(id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SpecialTag model)
        {
            if (ModelState.IsValid)
            {
                _context.SpecialTags.Update(model);
                _context.SaveChanges();
                TempData["Save"] = " Tag Update SuccessFully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var data = _context.SpecialTags.Find(id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(SpecialTag model)
        {
            if (ModelState.IsValid)
            {
                var data = _context.SpecialTags.Find(model.Id);
                _context.SpecialTags.Remove(data);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
