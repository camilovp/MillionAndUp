using Data;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MillionAndUpApi.DTO_s;
using MillionAndUpApi.Interfaces.Business;
using MillionAndUpApi.Interfaces.Mappers;
using MillionAndUpApi.Interfaces.Services;

namespace MillionAndUpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PropertyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPropertiesBusiness _propertiesBusiness;

        public PropertyController(
            ApplicationDbContext context,
            IPropertiesBusiness propertiesBusiness
            )
        {
            _context = context;
            _propertiesBusiness = propertiesBusiness;
        }

        /// <summary>
        /// Get Proporty By IdProperty
        /// </summary>
        /// <param name="id">IdProperty</param>
        /// <returns>PropertyDTO</returns>
        [HttpGet]
        [Route("property/{id}")]
        public async Task<IActionResult> GetPropertyById(string id) 
        {
            try
            {
                if (id == null) return BadRequest();
                var response = await _propertiesBusiness.GetPropertyById(Guid.Parse(id));
                if (response == null) return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(ex.HResult, ex);
            }
        }
        /// <summary>
        /// Get all property
        /// </summary>
        /// <returns>PropertyDTO</returns>

        [HttpGet]
        public async Task<IActionResult> GetAllProperty()
        {
            try
            {
                var response = await _propertiesBusiness.GetAllProperty();
                if (response == null) return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(ex.HResult, ex);
            }
        }

        /// <summary>
        /// Get Property By Filter
        /// </summary>
        /// <param name="propertyFilterDTO">Parameters and Value</param>
        /// <returns>PropertyDTO</returns>
        [HttpPost]
        [Route("propertybyfilter")]
        public async Task<IActionResult> GetPropertyByFilter([FromBody] PropertyFilterDTO propertyFilterDTO)
        {
            try
            {
                if (propertyFilterDTO == null) return BadRequest();
                var response = await _propertiesBusiness.GetAllByFilter(propertyFilterDTO);
                if (response == null) return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(ex.HResult, ex);
            }
        }

        /// <summary>
        /// Create Property
        /// </summary>
        /// <param name="propertyDTO">PropertyDTO</param>
        /// <returns><PropertyDTO/returns>
        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] PropertyDTO propertyDTO)
        {
            try
            {
                if (propertyDTO == null) return BadRequest();
                var response = await _propertiesBusiness.CreateProperty(propertyDTO);
                if (response == null) return BadRequest();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(ex.HResult, ex);
            }
        }

        /// <summary>
        /// Create Image
        /// </summary>
        /// <param name="propertyImageDTO">PropertyImageDTO</param>
        /// <returns>PropertyImage</returns>
        [HttpPost]
        [Route("propertyimage")]
        public async Task<IActionResult> CreatePropertyImage([FromForm] PropertyImageDTO propertyImageDTO)
        {
            try
            {
                if (propertyImageDTO == null) return BadRequest();
                var response = await _propertiesBusiness.CreatePropertyImage(propertyImageDTO);
                if (response == null) return BadRequest();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(ex.HResult, ex);
            }
        }

        /// <summary>
        /// Create Property Trace
        /// </summary>
        /// <param name="propertyTraceDTO">PropertyTraceDTO</param>
        /// <returns>PropertyTraceDTO</returns>

        [HttpPost]
        [Route("propertytrace")]
        public async Task<IActionResult> CreatePropertyTrace([FromBody] PropertyTraceDTO propertyTraceDTO)
        {
            try
            {
                if (propertyTraceDTO == null) return BadRequest();
                var response = await _propertiesBusiness.CreatePropertyTrace(propertyTraceDTO);
                if (response == null) return BadRequest();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(ex.HResult, ex);
            }
        }

        /// <summary>
        /// Update Property
        /// </summary>
        /// <param name="propertyDTO">PropertyDTO</param>
        /// <returns>PropertyDTO</returns>

        [HttpPut]

        public async Task<IActionResult> UpdateProperty([FromBody] PropertyDTO propertyDTO)
        {
            try
            {
                if (propertyDTO == null) return BadRequest();
                var response = await _propertiesBusiness.UpdateProperty(propertyDTO);
                if (response == null) return BadRequest();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(ex.HResult, ex);
            }
        }

        /// <summary>
        /// Patch Property
        /// </summary>
        /// <param name="id">Id Property</param>
        /// <param name="patchDocument">PropertyJson</param>
        /// <returns>200 Ok</returns>

        [HttpPatch]

        public async Task<IActionResult> PatchProperty(Guid id, JsonPatchDocument<Property> patchDocument)
        {
            try
            {
                if (patchDocument == null) return BadRequest();
                var validateProperty = _context.Property.FirstOrDefault(x => x.IdProperty == id);
                if (validateProperty == null) return NotFound();
                patchDocument.ApplyTo(validateProperty, ModelState);
                var isValid = TryValidateModel(validateProperty);
                if (!isValid) return BadRequest(ModelState);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(ex.HResult, ex);
            }
        }
    }
}
