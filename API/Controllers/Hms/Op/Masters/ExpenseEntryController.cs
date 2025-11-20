using System.Data;
using System.Reflection;
using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Services.Hms.Op.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseEntryController : ControllerBase
    {
        private readonly ExpenseEntryService? _service;
        public ExpenseEntryController(ExpenseEntryService service)
        {
            _service = service;
        }

        [HttpPost("SaveExpenseEntry")]
        public IActionResult SaveExpenseEntry([FromBody] ExpenseDto model)
        {
            try
            {
                return Ok(_service!.SaveExpenseEntry(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while saving expense entry",
                    error = ex.Message
                });
            }

        }

    }
}
