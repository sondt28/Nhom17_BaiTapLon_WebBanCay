using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nhom17_BaiTapLon_WebBanCayCanh.Data;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Controllers
{
    public class ProductOptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductOptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductOptions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductOptions.Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductOptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductOptions == null)
            {
                return NotFound();
            }

            var productOption = await _context.ProductOptions
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productOption == null)
            {
                return NotFound();
            }

            return View(productOption);
        }

        // GET: ProductOptions/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: ProductOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Value,Image,Price,StockQuantity,ProductId")] ProductOption productOption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productOption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productOption.ProductId);
            return View(productOption);
        }

        // GET: ProductOptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductOptions == null)
            {
                return NotFound();
            }

            var productOption = await _context.ProductOptions.FindAsync(id);
            if (productOption == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productOption.ProductId);
            return View(productOption);
        }

        // POST: ProductOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Value,Image,Price,StockQuantity,ProductId")] ProductOption productOption)
        {
            if (id != productOption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productOption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductOptionExists(productOption.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productOption.ProductId);
            return View(productOption);
        }

        // GET: ProductOptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductOptions == null)
            {
                return NotFound();
            }

            var productOption = await _context.ProductOptions
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productOption == null)
            {
                return NotFound();
            }

            return View(productOption);
        }

        // POST: ProductOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductOptions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductOptions'  is null.");
            }
            var productOption = await _context.ProductOptions.FindAsync(id);
            if (productOption != null)
            {
                _context.ProductOptions.Remove(productOption);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductOptionExists(int id)
        {
          return (_context.ProductOptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
