using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnicalTestPhanorMesias.DataModel;

namespace TechnicalTestPhanorMesias.Controllers
{
    public class TbPropertiesController : Controller
    {
        private readonly DbRealStateCompanyContext _context;

        public TbPropertiesController(DbRealStateCompanyContext context)
        {
            _context = context;
        }

        // GET: TbProperties
        public async Task<IActionResult> Index()
        {
            var dbRealStateCompanyContext = _context.TbProperties.Include(t => t.IdOwnerNavigation);
            return View(await dbRealStateCompanyContext.ToListAsync());
        }

        // GET: TbProperties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProperty = await _context.TbProperties
                .Include(t => t.IdOwnerNavigation)
                .FirstOrDefaultAsync(m => m.IdProperty == id);
            if (tbProperty == null)
            {
                return NotFound();
            }

            return View(tbProperty);
        }

        // GET: TbProperties/Create
        public IActionResult Create()
        {
            ViewData["IdOwner"] = new SelectList(_context.TbOwners, "IdOwner", "IdOwner");
            return View();
        }

        // POST: TbProperties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProperty,Name,Address,Price,CodeInternal,Year,IdOwner")] TbProperty tbProperty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbProperty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOwner"] = new SelectList(_context.TbOwners, "IdOwner", "IdOwner", tbProperty.IdOwner);
            return View(tbProperty);
        }

        // GET: TbProperties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProperty = await _context.TbProperties.FindAsync(id);
            if (tbProperty == null)
            {
                return NotFound();
            }
            ViewData["IdOwner"] = new SelectList(_context.TbOwners, "IdOwner", "IdOwner", tbProperty.IdOwner);
            return View(tbProperty);
        }

        // POST: TbProperties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProperty,Name,Address,Price,CodeInternal,Year,IdOwner")] TbProperty tbProperty)
        {
            if (id != tbProperty.IdProperty)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbProperty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbPropertyExists(tbProperty.IdProperty))
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
            ViewData["IdOwner"] = new SelectList(_context.TbOwners, "IdOwner", "IdOwner", tbProperty.IdOwner);
            return View(tbProperty);
        }

        // GET: TbProperties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProperty = await _context.TbProperties
                .Include(t => t.IdOwnerNavigation)
                .FirstOrDefaultAsync(m => m.IdProperty == id);
            if (tbProperty == null)
            {
                return NotFound();
            }

            return View(tbProperty);
        }

        // POST: TbProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbProperty = await _context.TbProperties.FindAsync(id);
            if (tbProperty != null)
            {
                _context.TbProperties.Remove(tbProperty);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbPropertyExists(int id)
        {
            return _context.TbProperties.Any(e => e.IdProperty == id);
        }
    }
}
