using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Services.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly DistrictService? _service;
        public DistrictsController(DistrictService service)
        {
            _service = service;
        }

        [HttpPost("SaveDistricts")]
        public IActionResult SaveDistricts([FromBody] District model)
        {
            try
            {
                return Ok(_service!.SaveDistricts(model));
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

        [HttpPost("GetDistricts")]
        public IActionResult GetDistricts([FromBody] District model)
        {
            try
            {
                return Ok(_service!.GetDistricts(model));
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


        [HttpPost("FillStates")]
        public IActionResult FillStates([FromBody] State model)
        {
            try
            {
                return Ok(_service!.FillStates(model));
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
