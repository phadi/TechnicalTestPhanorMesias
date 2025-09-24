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
    public class TbPropertyImagesController : Controller
    {
        private readonly DbRealStateCompanyContext _context;

        public TbPropertyImagesController(DbRealStateCompanyContext context)
        {
            _context = context;
        }

        // GET: TbPropertyImages
        public async Task<IActionResult> Index()
        {
            var dbRealStateCompanyContext = _context.TbPropertyImages.Include(t => t.IdImageTypeNavigation).Include(t => t.IdPropertyNavigation);
            return View(await dbRealStateCompanyContext.ToListAsync());
        }

        // GET: TbPropertyImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPropertyImage = await _context.TbPropertyImages
                .Include(t => t.IdImageTypeNavigation)
                .Include(t => t.IdPropertyNavigation)
                .FirstOrDefaultAsync(m => m.IdPropertyImage == id);
            if (tbPropertyImage == null)
            {
                return NotFound();
            }

            return View(tbPropertyImage);
        }

        // GET: TbPropertyImages/Create
        public IActionResult Create()
        {
            ViewData["IdImageType"] = new SelectList(_context.TbImageTypes, "IdImageType", "IdImageType");
            ViewData["IdProperty"] = new SelectList(_context.TbProperties, "IdProperty", "IdProperty");
            return View();
        }

        // POST: TbPropertyImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPropertyImage,IdProperty,IdImageType,FilePath,Enabled")] TbPropertyImage tbPropertyImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbPropertyImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdImageType"] = new SelectList(_context.TbImageTypes, "IdImageType", "IdImageType", tbPropertyImage.IdImageType);
            ViewData["IdProperty"] = new SelectList(_context.TbProperties, "IdProperty", "IdProperty", tbPropertyImage.IdProperty);
            return View(tbPropertyImage);
        }

        // GET: TbPropertyImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPropertyImage = await _context.TbPropertyImages.FindAsync(id);
            if (tbPropertyImage == null)
            {
                return NotFound();
            }
            ViewData["IdImageType"] = new SelectList(_context.TbImageTypes, "IdImageType", "IdImageType", tbPropertyImage.IdImageType);
            ViewData["IdProperty"] = new SelectList(_context.TbProperties, "IdProperty", "IdProperty", tbPropertyImage.IdProperty);
            return View(tbPropertyImage);
        }

        // POST: TbPropertyImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPropertyImage,IdProperty,IdImageType,FilePath,Enabled")] TbPropertyImage tbPropertyImage)
        {
            if (id != tbPropertyImage.IdPropertyImage)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbPropertyImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbPropertyImageExists(tbPropertyImage.IdPropertyImage))
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
            ViewData["IdImageType"] = new SelectList(_context.TbImageTypes, "IdImageType", "IdImageType", tbPropertyImage.IdImageType);
            ViewData["IdProperty"] = new SelectList(_context.TbProperties, "IdProperty", "IdProperty", tbPropertyImage.IdProperty);
            return View(tbPropertyImage);
        }

        // GET: TbPropertyImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPropertyImage = await _context.TbPropertyImages
                .Include(t => t.IdImageTypeNavigation)
                .Include(t => t.IdPropertyNavigation)
                .FirstOrDefaultAsync(m => m.IdPropertyImage == id);
            if (tbPropertyImage == null)
            {
                return NotFound();
            }

            return View(tbPropertyImage);
        }

        // POST: TbPropertyImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbPropertyImage = await _context.TbPropertyImages.FindAsync(id);
            if (tbPropertyImage != null)
            {
                _context.TbPropertyImages.Remove(tbPropertyImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbPropertyImageExists(int id)
        {
            return _context.TbPropertyImages.Any(e => e.IdPropertyImage == id);
        }
    }
}
