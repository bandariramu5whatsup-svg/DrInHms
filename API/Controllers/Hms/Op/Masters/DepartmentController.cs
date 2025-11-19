using HanuMediSoftCore.Models.Hms.Op.Masters;
using HanuMediSoftCore.Services.Hms.Op.Masters; 
using Microsoft.AspNetCore.Mvc; 

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        //private string ConnStr => ConnectionHelper.ConnectionString;
        private readonly DepartmentService? _service;
        public DepartmentController(DepartmentService service)
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


        [HttpPost("SaveDepartments")]
        public IActionResult SaveDepartments([FromBody] Department model)
        {
            try
            {
                return Ok(_service!.SaveDepartments(model));
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

        [HttpPost("GetDepartments")]
        public IActionResult GetDepartments([FromBody] Department model)
        {
            try
            {
                return Ok(_service!.GetDepartments(model));
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
