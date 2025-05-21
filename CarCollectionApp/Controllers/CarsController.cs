using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarCollectionApp.Models;
using CarCollectionApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace CarCollectionApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarCollectionContext _context;
        private readonly ICarStatsService _carStatsService;

        public CarsController(CarCollectionContext context, ICarStatsService carStatsService)
        {
            _context = context;
            _carStatsService = carStatsService;
        }

        // GET: Cars
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["HpSortParm"] = sortOrder == "HP" ? "hp_desc" : "HP";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            var cars = _context.Cars.Include(c => c.Brand).Include(c => c.Category).AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(c => c.Model.Contains(searchString) || c.Brand.Name.Contains(searchString));
            }

            cars = sortOrder switch
            {
                "name_desc" => cars.OrderByDescending(c => c.Model),
                "HP" => cars.OrderBy(c => c.Horsepower),
                "hp_desc" => cars.OrderByDescending(c => c.Horsepower),
                "Price" => cars.OrderBy(c => c.Price),
                "price_desc" => cars.OrderByDescending(c => c.Price),
                _ => cars.OrderBy(c => c.Model),
            };

            return View(await cars.ToListAsync());
        }

        // GET: Cars/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            ViewBag.Performance = _carStatsService.EvaluatePerformance(car.Horsepower, car.Price);

            return View(car);
        }

        // GET: Cars/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Type");
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,Engine,Horsepower,Price,BrandId,CategoryId")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", car.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Type", car.CategoryId);
            return View(car);
        }

        // GET: Cars/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", car.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Type", car.CategoryId);
            return View(car);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,Engine,Horsepower,Price,BrandId,CategoryId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", car.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Type", car.CategoryId);
            return View(car);
        }

        // GET: Cars/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
