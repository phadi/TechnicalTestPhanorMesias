using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealStateDataModel.DataModel;
using RealStateDataModel.DTOs;
using RealStateService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalTestPhanorMesias.Models;

namespace TechnicalTestPhanorMesias.Controllers
{
    public class TbPropertiesController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IOwnerService _ownerService;

        public TbPropertiesController(IPropertyService propertyService, IOwnerService ownerService)
        {
            _propertyService = propertyService;
            _ownerService = ownerService;
        }

        // GET: TbProperties
        public async Task<IActionResult> Index(RealStateDataModel.Models.PropertyIndex propertyIndexParam)
        {
            var dbRealStateCompanyContext = await _propertyService.FilterProperties(propertyIndexParam);
            RealStateDataModel.Models.PropertyIndex propertyIndex = new RealStateDataModel.Models.PropertyIndex();
            propertyIndex.Properties = dbRealStateCompanyContext;
           
            return View(propertyIndex);
        }

        // GET: TbProperties/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var tbProperty = await _propertyService.GetPropertyDetails(id);

            return View(tbProperty);
        }

        // GET: TbProperties/Create
        public IActionResult Create()
        {
            ViewData["IdOwner"] = new SelectList(_ownerService.GetOwners(), "IdOwner", "FullName");
            return View();
        }

        // POST: TbProperties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("IdProperty,Name,Address,Price,CodeInternal,Year,IdOwner")] TbProperty tbProperty)
        {
            try
            {
                await _propertyService.CreateProperty(tbProperty);
                return RedirectToAction(nameof(Index));
            }
            catch{ }
            
            ViewData["IdOwner"] = new SelectList(_ownerService.GetOwners(), "IdOwner", "FullName", tbProperty.IdOwner);
            return View(tbProperty);
        }

        // GET: TbProperties/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var tbProperty = await _propertyService.GetPropertyDTOById(id);
            if (tbProperty == null)
            {
                return NotFound();
            }

            ViewData["IdOwner"] = new SelectList(_ownerService.GetOwners(), "IdOwner", "FullName", tbProperty.IdOwner);
            return View(tbProperty);
        }

        // POST: TbProperties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("IdProperty,Name,Address,Price,CodeInternal,Year,IdOwner")] TbPropertyDTO tbProperty, IFormFile imagenSubida)
        {
            if (id != tbProperty.IdProperty)
            {
                return NotFound();
            }

            try
            {
                if (imagenSubida != null && imagenSubida.Length > 0)
                {
                    await UploadImage(imagenSubida, id);
                }

                await _propertyService.UpdatePropertyDTO(tbProperty);
                return RedirectToAction(nameof(Index));
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
        }

        private async Task UploadImage(IFormFile imagenSubida, int id)
        {
            var nombreUnico = id.ToString() + "_" + Guid.NewGuid().ToString() + "_" + Path.GetFileName(imagenSubida.FileName);
            using (var memoryStream = new MemoryStream())
            {
                await imagenSubida.CopyToAsync(memoryStream);
                var ContenidoImagen = memoryStream.ToArray();
                await _propertyService.SaveImage(id, nombreUnico, ContenidoImagen);
            }
        }

        private bool TbPropertyExists(int id)
        {
            return _propertyService.GetTbProperties().Any(e => e.IdProperty == id);
        }
    }
}
