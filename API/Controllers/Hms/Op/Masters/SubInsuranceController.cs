using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Services.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubInsuranceController : Controller
    {
        private readonly SubInsuranceService? _service;
        public SubInsuranceController(SubInsuranceService service)
        {
            _service = service;
        }

        [HttpPost("SaveSubInsurance")]
        public IActionResult SaveSubInsurance([FromBody] SubInsurance model)
        {
            try
            {
                return Ok(_service!.SaveSubInsurance(model));
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

        [HttpPost("GetSubInsurance")]
        public IActionResult GetSubInsurance([FromBody] SubInsurance model)
        {
            try
            {
                return Ok(_service!.GetSubInsurance(model));
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
