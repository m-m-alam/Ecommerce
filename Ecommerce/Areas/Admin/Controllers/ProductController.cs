using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _host;
        public ProductController(ApplicationDbContext context, IHostingEnvironment host)
        {
            _context = context;
            _host = host;
        }
        public IActionResult Index()
        {
            var data = _context.Products.Include(x => x.ProductType).Include(x => x.SpecialTag).ToList();
            return View(data);
        }

        public IActionResult Index(decimal lowAmount, decimal largeAmount)
        {
            var products = _context.Products.Include(x => x.ProductType).Include(x => x.SpecialTag).Where(x => x.Price >= lowAmount && x.Price <= largeAmount).ToList();
            return View();
        }
        public IActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_context.ProductTypes.ToList(), "Id", "Name");
            ViewData["TagId"] = new SelectList(_context.SpecialTags.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product model, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _context.Products.FirstOrDefault(x => x.Name == model.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This Product already exist";
                    ViewData["productTypeId"] = new SelectList(_context.ProductTypes.ToList(), "Id", "Name");
                    ViewData["TagId"] = new SelectList(_context.SpecialTags.ToList(), "Id", "Name");
                    return View(model);
                }
                if(image != null)
                {
                    var name = Path.Combine(_host.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    image.CopyTo(new FileStream(name, FileMode.Create));
                    model.Image = "images/" + image.FileName;
                }
                _context.Products.Add(model);
                _context.SaveChanges();
                TempData["Save"] = " Product Save SuccessFully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            ViewData["productTypeId"] = new SelectList(_context.ProductTypes.ToList(), "Id", "Name");
            ViewData["TagId"] = new SelectList(_context.SpecialTags.ToList(), "Id", "Name");
            //var data = _context.Products.Find(id);
            var data = _context.Products.Include(x => x.ProductType).Include(x => x.SpecialTag).FirstOrDefault(c => c.Id == id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product model, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_host.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    image.CopyTo(new FileStream(name, FileMode.Create));
                    model.Image = "images/" + image.FileName;
                }
                _context.Products.Update(model);
                _context.SaveChanges();
                TempData["Save"] = " Product Update SuccessFully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var data = _context.Products.Find(id);
            //var data = _context.Products.Include(c => c.ProductType).Include(c => c.SpecialTag).Where(c => c.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(SpecialTag model)
        {
            if (ModelState.IsValid)
            {
                var data = _context.Products.Find(model.Id);
                _context.Products.Remove(data);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
