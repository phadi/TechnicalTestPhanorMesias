using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealStateApi.Helpers;
using RealStateDataModel.DataModel;
using RealStateDataModel.Models;
using RealStateService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TbPropertiesController : ControllerBase
    {
        private readonly IPropertyApiService _propertyService;
        private readonly IOwnerService _ownerService;

        public TbPropertiesController(IPropertyApiService propertyService, IOwnerService ownerService)
        {
            _propertyService = propertyService;
            _ownerService = ownerService;
        }

        [HttpGet(Name = "GetTbProperties")]
        public async Task<ActionResult<IEnumerable<TbProperty>>> GetTbProperties()
        {
            return await _propertyService.GetProperties();
        }

        [HttpGet] 
        [Route("GetFilteredTbProperties")]
        public async Task<ActionResult<IEnumerable<TbProperty>>> GetFilteredTbProperties([FromQuery] PropertyFilters filters)
        {
            return await _propertyService.GetProperties(filters);

        }

        [HttpPut]
        [Route("UpdateProperty")]
        public async Task<IActionResult> UpdateProperty(int id, UpdatedProperty tbProperty)
        {
            try
            {
                var resp = await _propertyService.UpdateProperty(id, tbProperty);
                if (resp == 0)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpPatch]
        [Route("ChangePrice")]
        public async Task<IActionResult> ChangePrice(int id, decimal price)
        {
            try
            {
                var resp = await _propertyService.ChangePrice(id, price);
                if (resp == 0)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpPost]
        [Route("CreateProperty")]
        public async Task<ActionResult<TbProperty>> CreateProperty(CreatedProperty tbProperty)
        {
            var newProperty = await _propertyService.CreateProperty(tbProperty);
            return Ok(newProperty);
        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file, int id) 
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No se seleccionó ningún archivo.");
            }

            string response = await SaveImage(file, id);

            if (string.IsNullOrEmpty(response))
                return Ok(new { message = "Imagen subida exitosamente", fileName = Guid.NewGuid().ToString() + "_" + file.FileName });
            else
                return StatusCode(500, "Ha ocurrido un error al subir la imagen");
        }

        private async Task<string> SaveImage(IFormFile file, int id) 
        {
            try
            {
                var nombreUnico = id.ToString() + "_" + Guid.NewGuid().ToString() + "_" + file.FileName;

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var ContenidoImagen = memoryStream.ToArray();
                    await _propertyService.SaveImage(id, nombreUnico, ContenidoImagen);
                }
                return string.Empty;
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }
    }
}
