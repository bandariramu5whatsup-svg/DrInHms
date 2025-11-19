using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Services.Hms.Op.Masters; 
using Microsoft.AspNetCore.Mvc; 

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecilizationController : ControllerBase
    {
        //private string ConnStr => ConnectionHelper.ConnectionString;
        private readonly SpecilizationService? _service;
        public SpecilizationController(SpecilizationService service)
        {
            _service = service;
        }

      

        [HttpPost("SaveSpecilizations")]
        public IActionResult SaveDepartments([FromBody] Specialization model)
        {
            try
            {
                return Ok(_service!.SaveSpecilizations(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while fetching units",
                    error = ex.Message
                });
            }
        }

        [HttpPost("GetSpecilizations")]
        public IActionResult GetSpecilizations([FromBody] Specialization model)
        {
            try
            {
                return Ok(_service!.GetSpecilizations(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while fetching units",
                    error = ex.Message
                });
            }
        }
    }
}
