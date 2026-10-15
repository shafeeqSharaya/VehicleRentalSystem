using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleRentalSystem.Data;
using VehicleRentalSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace VehicleRentalSystem.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["BrandSort"] = String.IsNullOrEmpty(sortOrder) ? "brand_desc" : "";
            ViewData["PriceSort"] = sortOrder == "price" ? "price_desc" : "price";

            var cars = _context.Cars
                .Include(c => c.Branch)
                .Include(c => c.CarCategory)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(c =>
                    c.Brand.Contains(searchString) ||
                    c.Model.Contains(searchString) ||
                    c.PlateNumber.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "brand_desc":
                    cars = cars.OrderByDescending(c => c.Brand);
                    break;

                case "price":
                    cars = cars.OrderBy(c => c.DailyPrice);
                    break;

                case "price_desc":
                    cars = cars.OrderByDescending(c => c.DailyPrice);
                    break;

                default:
                    cars = cars.OrderBy(c => c.Brand);
                    break;
            }

            return View(await cars.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Branch)
                .Include(c => c.CarCategory)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchName");
            ViewData["CarCategoryId"] = new SelectList(_context.CarCategories, "CarCategoryId", "CategoryName");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,Brand,Model,ManufactureYear,DailyPrice,PlateNumber,BranchId,CarCategoryId")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchName", car.BranchId);
            ViewData["CarCategoryId"] = new SelectList(_context.CarCategories, "CarCategoryId", "CategoryName", car.CarCategoryId);
            return View(car);
        }

        // GET: Cars/Edit/5
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
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchName", car.BranchId);
            ViewData["CarCategoryId"] = new SelectList(_context.CarCategories, "CarCategoryId", "CategoryName", car.CarCategoryId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,Brand,Model,ManufactureYear,DailyPrice,PlateNumber,BranchId,CarCategoryId")] Car car)
        {
            if (id != car.CarId)
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
                    if (!CarExists(car.CarId))
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
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchName", car.BranchId);
            ViewData["CarCategoryId"] = new SelectList(_context.CarCategories, "CarCategoryId", "CategoryName", car.CarCategoryId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Branch)
                .Include(c => c.CarCategory)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
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
            return _context.Cars.Any(e => e.CarId == id);
        }
    }
}
