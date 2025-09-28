using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealStateDataModel.DataModel;
using TechnicalTestPhanorMesias.Models;
using RealStateService.Interfaces;
using RealStateDataModel.DTOs;

namespace TechnicalTestPhanorMesias.Controllers
{
    public class TbPropertiesController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IOwnerService _ownerService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public TbPropertiesController(IPropertyService propertyService, IOwnerService ownerService, IWebHostEnvironment hostingEnvironment)
        {
            _propertyService = propertyService;
            _ownerService = ownerService;
            _hostingEnvironment = hostingEnvironment;
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

        public ActionResult ObtenerImagen()
        {
            // Lógica para obtener la ruta o los bytes de la imagen
            string rutaImagen = "/images/2_CasaProp1"; // o bytes desde BD

            // Devuelve el archivo de imagen
            return File(rutaImagen, "image/png"); // El segundo parámetro es el contentType
        }

        
        //public async Task<IActionResult> GuardarImagen()
        //{
        //    return View("Edit");
        //}

        //[HttpPost]
        public async Task<IActionResult> GuardarImagen(IFormFile imagenSubida)
        {
            if (imagenSubida != null && imagenSubida.Length > 0)
            {
                // Opción 1: Guardar en el sistema de archivos
                var nombreUnico = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imagenSubida.FileName);
                var rutaCarpeta = Path.Combine(_hostingEnvironment.WebRootPath, "Images"); // Crear una carpeta 'imagenes' en wwwroot
                Directory.CreateDirectory(rutaCarpeta); // Asegurarse de que la carpeta existe

                var rutaCompletaArchivo = Path.Combine(rutaCarpeta, nombreUnico);

                using (var stream = new FileStream(rutaCompletaArchivo, FileMode.Create))
                {
                    await imagenSubida.CopyToAsync(stream);
                }

                //// Aquí puedes guardar la ruta en tu base de datos
                //var miModelo = new MiModeloImagen
                //{
                //    NombreImagen = nombreUnico,
                //    RutaArchivo = $"~/imagenes/{nombreUnico}" // Guarda la ruta relativa para mostrarla
                //};
                //// ... guardar miModelo en la base de datos ...

                return RedirectToAction("Edit"); // Redirige a otra acción

                // Opción 2: Guardar como arreglo de bytes en la base de datos
                // using (var memoryStream = new MemoryStream())
                // {
                //     await imagenSubida.CopyToAsync(memoryStream);
                //     var miModelo = new MiModeloImagen
                //     {
                //         NombreImagen = Path.GetFileName(imagenSubida.FileName),
                //         ContenidoImagen = memoryStream.ToArray()
                //     };
                //     // ... guardar miModelo en la base de datos ...
                // }
            }
            // Manejar el caso en que no se sube ningún archivo o es inválido
            return View("Edit");
        }

        // GET: TbProperties/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var tbProperty = await _propertyService.GetPropertyDTOById(id, _hostingEnvironment.ContentRootPath);
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
        public async Task<IActionResult> Edit(int id, [Bind("IdProperty,Name,Address,Price,CodeInternal,Year,IdOwner")] TbPropertyDTO tbProperty)
        {
            if (id != tbProperty.IdProperty)
            {
                return NotFound();
            }

            try
            {
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

            ViewData["IdOwner"] = new SelectList(_ownerService.GetOwners(), "IdOwner", "FullName", tbProperty.IdOwner);
            return View(tbProperty);
        }

        private bool TbPropertyExists(int id)
        {
            return _propertyService.GetTbProperties().Any(e => e.IdProperty == id);
        }
    }
}
