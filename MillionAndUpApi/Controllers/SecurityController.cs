using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MillionAndUpApi.DTO_s;
using MillionAndUpApi.Interfaces.Business;

namespace MillionAndUpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityBusiness _securityBusiness;

        public SecurityController(ISecurityBusiness securityBusiness)
        {
            _securityBusiness = securityBusiness;
        }
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] CredentialsDTO credentialsDTO)
        {
            try
            {
                if (credentialsDTO == null) return BadRequest();
                var result = await _securityBusiness.GetToken(credentialsDTO);
                if (result == null) return BadRequest();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(ex.HResult, ex);
            }
        }
    }
}
