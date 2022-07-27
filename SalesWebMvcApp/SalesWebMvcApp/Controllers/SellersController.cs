using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMvcApp.Models;
using SalesWebMvcApp.Models.ViewsModels;
using SalesWebMvcApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvcApp.Controllers
{
    public class SellersController : Controller
    {
        private readonly SalesWebMvcAppContext _context;
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SalesWebMvcAppContext context, SellerService sellerService, DepartmentService departmentService)
        {
            _context = context;
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // GET: Seller
        public async Task<IActionResult> Index()
        {
            // return View(await _context.Seller.ToListAsync());
            var list = _sellerService.FindAll();
            return View(list);
          
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided " });


            var obj = _sellerService.FindById(id.Value);

            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Id not found " });

            return View(obj);
        }

        // GET: Sallers/Create
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments};
            return View(viewModel);
        }

        // POST: Sallers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Saller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        // GET: Sallers/Edit/?id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var obj = _sellerService.FindById(id.Value);
            if (obj == null) return NotFound();

            List<Departments> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        // POST: Sallers/Edit/?id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Saller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

        // Get: Sellers/Delete/?id
        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided"});

            var sellers = _sellerService.FindById(id.Value);

            if (sellers == null) return RedirectToAction(nameof(Error), new { message = "Id not found" });

            return View(sellers);
        }

        // POST: Sallers/Delete/?id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }


        private bool SellersExists(int id)
        {
            return _context.Seller.Any(e => e.Id == id);
        }
    }
}
