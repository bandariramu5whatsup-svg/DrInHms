using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Services.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesPurposeController : Controller
    {
        private readonly ExpensesPurposeService? _service;
        public ExpensesPurposeController(ExpensesPurposeService service)
        {
            _service = service;
        }

        [HttpPost("SaveExpensesPurpose")]
        public IActionResult SaveExpensesPurpose([FromBody] ExpensesPurpose model)
        {
            try
            {
                return Ok(_service!.SaveExpensesPurpose(model));
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

        [HttpPost("GetExpensesPurpose")]
        public IActionResult GetExpensesPurpose([FromBody] ExpensesPurpose model)
        {
            try
            {
                return Ok(_service!.GetExpensesPurpose(model));
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
