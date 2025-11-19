using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc;
using static HanuMediSoftCore.Helpers.Helpers;

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : ControllerBase
    {
        private string ConnStr => ConnectionHelper.ConnectionString;


        [HttpPost("SaveServiceType")]
        public IActionResult SaveServiceType([FromBody] ServiceType model)
        {
            try
            {
                var parameters = new Dictionary<string, object?>
                {
                    { "@ServiceTypeId", model.ServiceTypeId },
                    { "@ServiceTypeName", model.ServiceTypeName },
                    { "@Description", model.Description },
                    { "@IsActive", model.IsActive },
                    { "@UserName", model.CreatedByName },
                    { "@UserId", model.CreatedById },
                    { "@WorkstationId", model.WorkstationId }
                };


                var dt = ExecuteSP(ConnStr, "SpOpInsertServiceType", parameters);
                var list = ToList(dt);

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost("GetServiceTypes")]
        public IActionResult GetServiceTypes([FromBody] ServiceType model)
        {
            try
            {
                var parameters = new Dictionary<string, object?>
                {
                    { "@ServiceTypeId", model.ServiceTypeId },
                    { "@ServiceTypeName", model.ServiceTypeName },
                    { "@IsActive", model.IsActive },
                    //{ "@PageIndex", model.PageIndex },
                    //{ "@PageSize", model.PageSize }
                };

                var dt = ExecuteSP(ConnStr, "SpOpGetServiceTypes", parameters);
                var list = ToList(dt);

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
