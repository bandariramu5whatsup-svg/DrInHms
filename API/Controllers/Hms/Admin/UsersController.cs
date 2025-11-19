using HanuMediSoftCore.Models.Hms.Admin;
using Microsoft.AspNetCore.Mvc;
using static HanuMediSoftCore.Helpers.Helpers;
namespace HanuMediSoftCore.API.Controllers.Hms.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private string ConnStr => ConnectionHelper.ConnectionString;

        [HttpPost("ValidateUserLogin")]
        public IActionResult ValidateUserLogin([FromBody] Users model)
        {
            try
            {
                var parameters = new Dictionary<string, object?>
                {
                    { "@UserName", model.UserName },
                    { "@Password", model.Password }
                };

                var dt = ExecuteSP(ConnStr, "spValidateUserLogin", parameters);
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
