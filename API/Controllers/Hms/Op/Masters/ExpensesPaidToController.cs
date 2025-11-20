using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Services.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesPaidToController : Controller
    {
        private readonly ExpensesPaidToService? _service;
        public ExpensesPaidToController(ExpensesPaidToService service)
        {
            _service = service;
        }

        [HttpPost("SaveExpensesPaidTo")]
        public IActionResult SaveExpensesPaidTo([FromBody] ExpensesPaidTo model)
        {
            try
            {
                return Ok(_service!.SaveExpensesPaidTo(model));
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

        [HttpPost("GetExpensesPaidTo")]
        public IActionResult GetExpensesPaidTo([FromBody] ExpensesPaidTo model)
        {
            try
            {
                return Ok(_service!.GetExpensesPaidTo(model));
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
