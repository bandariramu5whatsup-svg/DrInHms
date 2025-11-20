using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Services.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesTypesController : ControllerBase
    {
        private readonly ExpensesTypesService? _service;
        public ExpensesTypesController(ExpensesTypesService service)
        {
            _service = service;
        }

        [HttpPost("SaveExpensesTypes")]
        public IActionResult SaveExpensesTypes([FromBody] ExpensesType model)
        {
            try
            {
                return Ok(_service!.SaveExpensesTypes(model));
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

        [HttpPost("GetExpensesTypes")]
        public IActionResult GetExpensesTypes([FromBody] ExpensesType model)
        {
            try
            {
                return Ok(_service!.GetExpensesTypes(model));
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
