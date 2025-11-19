using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Services.Hms.Op.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly CountriesService? _service;
        public CountriesController(CountriesService service)
        {
            _service = service;
        }

        [HttpPost("SaveCountries")]
        public IActionResult SaveCountries([FromBody] Country model)
        {
            try
            {
                return Ok(_service!.SaveCountries(model));
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

        [HttpPost("GetCountries")]
        public IActionResult GetCountries([FromBody] Country model)
        {
            try
            {
                return Ok(_service!.GetCountries(model));
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
