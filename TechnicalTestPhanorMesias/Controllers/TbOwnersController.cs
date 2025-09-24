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
    public class TbOwnersController : Controller
    {
        private readonly DbRealStateCompanyContext _context;

        public TbOwnersController(DbRealStateCompanyContext context)
        {
            _context = context;
        }

        // GET: TbOwners
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbOwners.ToListAsync());
        }

        // GET: TbOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbOwner = await _context.TbOwners
                .FirstOrDefaultAsync(m => m.IdOwner == id);
            if (tbOwner == null)
            {
                return NotFound();
            }

            return View(tbOwner);
        }

        // GET: TbOwners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbOwners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Address,LastName,FirstName,IdOwner,Photo,Birthday")] TbOwner tbOwner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbOwner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbOwner);
        }

        // GET: TbOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbOwner = await _context.TbOwners.FindAsync(id);
            if (tbOwner == null)
            {
                return NotFound();
            }
            return View(tbOwner);
        }

        // POST: TbOwners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Address,LastName,FirstName,IdOwner,Photo,Birthday")] TbOwner tbOwner)
        {
            if (id != tbOwner.IdOwner)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbOwnerExists(tbOwner.IdOwner))
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
            return View(tbOwner);
        }

        // GET: TbOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbOwner = await _context.TbOwners
                .FirstOrDefaultAsync(m => m.IdOwner == id);
            if (tbOwner == null)
            {
                return NotFound();
            }

            return View(tbOwner);
        }

        // POST: TbOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbOwner = await _context.TbOwners.FindAsync(id);
            if (tbOwner != null)
            {
                _context.TbOwners.Remove(tbOwner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbOwnerExists(int id)
        {
            return _context.TbOwners.Any(e => e.IdOwner == id);
        }
    }
}
