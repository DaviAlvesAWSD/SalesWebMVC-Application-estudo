using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvcApp.Controllers
{
    public class SellersController : Controller
    {
        private readonly SalesWebMvcAppContext _context;

        public SellersController(SalesWebMvcAppContext context)
        {
            _context = context;
        }

        // GET: Seller
        public async Task<IActionResult> Index()
        {
            return View(await _context.Seller.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null) return NotFound();


            var sellers = await _context.Seller
                    .FirstOrDefaultAsync(seller => seller.Id == id);

            if (sellers == null) return NotFound();

            return View(sellers);
        }

        // GET: Sallers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sallers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Email, BirthDate, BaseSalary,Department")] Saller sellers)
        {
            if(ModelState.IsValid)
            {
                _context.Add(sellers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(sellers);
        }

        // GET: Sallers/Edit/?id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sellers = await _context.Seller.FindAsync(id);
            if (sellers == null) return NotFound();

            return View(sellers);
        }

        // POST: Sallers/Edit/?id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Email, BirthDate, BaseSalary,Department")] Saller sellers)
        {
            if (id != sellers.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sellers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellersExists(sellers.Id))
                    {
                        return NotFound();
                    }
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sellers);
        }

        // Get: Sellers/Delete/?id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) NotFound();

            var sellers = await _context.Seller
                .FirstOrDefaultAsync(seller => seller.Id == id);
            if (sellers == null) NotFound();

            return View(sellers);
        }

        // POST: Sallers/Delete/?id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sellers = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(sellers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool SellersExists(int id)
        {
            return _context.Seller.Any(e => e.Id == id);
        }
    }
}
