using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShadiSystemsTask.Models;

namespace ShadiSystemsTask.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly TestDBContext _context;

        public CategoriesController(TestDBContext context)
        {
            _context = context;
        }

        // GET: Categories
        public IActionResult Index()
        {
            var cat = _context.Categories.Include(x => x.Items);
            
            return View(cat);
        }
        [HttpGet]
        public IActionResult Index(string searchItem)
        {
            List<Category> CategoryList = _context.Categories.Include(E=>E.Items).ToList();
            if (string.IsNullOrEmpty(searchItem))
            {
                CategoryList = _context.Categories.ToList();
            }
            else
            {
                CategoryList = _context.Categories.Where(x => x.CatName.StartsWith(searchItem)).ToList();
            }
                ViewBag.CategoryList = new SelectList(CategoryList, "CatId", "CatName");
            return View(CategoryList);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            var depts = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(depts, "CatId", "CatName", 1);
            return View();
        }
        // POST: Categories/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CatId,CatName")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                 _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatId,CatName")] Category category)
        {
            if (id != category.CatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CatId))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CatId == id);
        }
    }
}
