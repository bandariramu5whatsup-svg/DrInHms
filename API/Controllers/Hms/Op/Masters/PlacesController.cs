using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Services.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly PlacesService? _service;
        public PlacesController(PlacesService service)
        {
            _service = service;
        }

        [HttpPost("SavePlaces")]
        public IActionResult SavePlaces([FromBody] Place model)
        {
            try
            {
                return Ok(_service!.SavePlaces(model));
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

        [HttpPost("GetPlaces")]
        public IActionResult GetPlaces([FromBody] Place model)
        {
            try
            {
                return Ok(_service!.GetPlaces(model));
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
