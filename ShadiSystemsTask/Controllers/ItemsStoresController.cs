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
    public class ItemsStoresController : Controller
    {
        private readonly TestDBContext _context;

        public ItemsStoresController(TestDBContext context)
        {
            _context = context;
        }

        // GET: ItemsStores
        public async Task<IActionResult> Index()
        {
            var testDBContext = _context.ItemsStores.Include(i => i.Item);
            return View(await testDBContext.ToListAsync());
        }

        // GET: ItemsStores/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsStore = await _context.ItemsStores
                .Include(i => i.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemsStore == null)
            {
                return NotFound();
            }

            return View(itemsStore);
        }

        // GET: ItemsStores/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemName", "ItemName");
            return View();
        }

        // POST: ItemsStores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemId,Quantity")] ItemsStore itemsStore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemsStore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId", itemsStore.ItemId);
            return View(itemsStore);
        }

        // GET: ItemsStores/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsStore = await _context.ItemsStores.FindAsync(id);
            if (itemsStore == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId", itemsStore.ItemId);
            return View(itemsStore);
        }

        // POST: ItemsStores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ItemId,Quantity")] ItemsStore itemsStore)
        {
            if (id != itemsStore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemsStore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsStoreExists(itemsStore.Id))
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
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId", itemsStore.ItemId);
            return View(itemsStore);
        }

        // GET: ItemsStores/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsStore = await _context.ItemsStores
                .Include(i => i.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemsStore == null)
            {
                return NotFound();
            }

            return View(itemsStore);
        }

        // POST: ItemsStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var itemsStore = await _context.ItemsStores.FindAsync(id);
            _context.ItemsStores.Remove(itemsStore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemsStoreExists(long id)
        {
            return _context.ItemsStores.Any(e => e.Id == id);
        }
    }
}
