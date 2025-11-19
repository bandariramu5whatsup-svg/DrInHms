using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Services.Hms.Op.Masters; 
using Microsoft.AspNetCore.Mvc; 

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        //private string ConnStr => ConnectionHelper.ConnectionString;
        private readonly UnitsService? _service;
        public UnitsController(UnitsService service)
        {
            _service = service;
        }

        //[HttpPost("SaveUnits")]
        //public IActionResult SaveUnits([FromBody] Unit model)
        //{



        //    try
        //    {
        //        var parameters = new Dictionary<string, object?>
        //        {
        //            { "@UnitId", model.UnitId },
        //            { "@UnitName", model.UnitName },
        //            { "@Description", model.Description },
        //            { "@IsActive", model.IsActive },
        //            { "@UserName", model.CreatedByName },
        //            { "@UserId", model.CreatedById },
        //            { "@WorkstationId", model.WorkstationId }
        //        };


        //        var dt = ExecuteSP(ConnStr, "SpOpInsertUnit", parameters);
        //        var list = ToList(dt);

        //        return Ok(list);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.ToString());
        //    }
        //}


        [HttpPost("SaveUnits")]
        public IActionResult SaveUnits([FromBody] Unit model)
        {
            try
            {
                return Ok(_service!.SaveUnits(model));
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

        [HttpPost("GetUnits")]
        public IActionResult GetUnits([FromBody] Unit model)
        {
            try
            {
                return Ok(_service!.GetUnits(model));
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
