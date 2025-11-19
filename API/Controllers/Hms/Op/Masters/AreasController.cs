using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Services.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly AreasService? _service;
        public AreasController(AreasService service)
        {
            _service = service;
        }

        //ramu begin

        [HttpPost("SaveAreas")]
        public IActionResult SaveAreas([FromBody] Area model)
        {
            try
            {
                return Ok(_service!.SaveAreas(model));
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

        [HttpPost("GetAreas")]
        public IActionResult GetAreas([FromBody] Area model)
        {
            try
            {
                return Ok(_service!.GetAreas(model));
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
