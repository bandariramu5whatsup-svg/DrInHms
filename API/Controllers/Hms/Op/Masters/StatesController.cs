using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Services.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly StatesService? _service;
        public StatesController(StatesService service)
        {
            _service = service;
        }

        [HttpPost("SaveStates")]
        public IActionResult SaveStates([FromBody] State model)
        {
            try
            {
                return Ok(_service!.SaveStates(model));
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

        [HttpPost("GetStates")]
        public IActionResult GetStates([FromBody] State model)
        {
            try
            {
                return Ok(_service!.GetStates(model));
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


        [HttpPost("FillCountries")]
        public IActionResult FillCountries([FromBody] State model)
        {
            try
            {
                return Ok(_service!.FillCountries(model));
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
