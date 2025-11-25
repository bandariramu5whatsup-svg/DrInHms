using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Services.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayCategoriesController : Controller
    {
        private readonly PayCategoriesService? _service;
        public PayCategoriesController(PayCategoriesService service)
        {
            _service = service;
        }

        [HttpPost("SavePayCategories")]
        public IActionResult SavePayCategories([FromBody] PayCategories model)
        {
            try
            {
                return Ok(_service!.SavePayCategories(model));
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

        [HttpPost("GetPayCategories")]
        public IActionResult GetPayCategories([FromBody] PayCategories model)
        {
            try
            {
                return Ok(_service!.GetPayCategories(model));
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
