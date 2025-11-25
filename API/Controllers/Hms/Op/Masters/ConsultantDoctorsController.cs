using HanuMediSoftCore.Models.Hms.Op.Masters;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HanuMediSoftCore.API.Controllers.Hms.Op.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantDoctorsController : ControllerBase
    {
        private readonly IConfiguration _config;
        public ConsultantDoctorsController(IConfiguration config) => _config = config;

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] ConsultantDoctorDto dto)
        {
            var connStr = _config.GetConnectionString("DefaultConnection"); // set in appsettings.json

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("sp_SaveConsultantDoctor", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters (only a subset shown; add all used by SP)
                cmd.Parameters.AddWithValue("@ConsultantDoctorId", (object)dto.ConsultantDoctorId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ConsultantDoctorName", (object)dto.ConsultantDoctorName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DoctorRegNo", (object)dto.DoctorRegNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DepartmentId", (object)dto.DepartmentId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SpecilizationId", (object)dto.SpecilizationId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Qualification", (object)dto.Qualification ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MobileNO", (object)dto.MobileNO ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Age", dto.Age.HasValue ? (object)dto.Age.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Gender", dto.Gender.HasValue ? (object)dto.Gender.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@ConsultTimeInMinutes", dto.ConsultTimeInMinutes.HasValue ? (object)dto.ConsultTimeInMinutes.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@ConsultTimeFromInterval", dto.ConsultTimeFromInterval.HasValue ? (object)dto.ConsultTimeFromInterval.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@ConsultTimeToInterval", dto.ConsultTimeToInterval.HasValue ? (object)dto.ConsultTimeToInterval.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Active", dto.Active ? (object)dto.Active : DBNull.Value);

                cmd.Parameters.AddWithValue("@GeneralConsultationFee", dto.GeneralConsultationFee.HasValue ? (object)dto.GeneralConsultationFee.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@GeneralFeeToHospital", dto.GeneralFeeToHospital.HasValue ? (object)dto.GeneralFeeToHospital.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@GeneralFeeToDoctor", dto.GeneralFeeToDoctor.HasValue ? (object)dto.GeneralFeeToDoctor.Value : DBNull.Value);

                // ... add remaining fee params similarly

                cmd.Parameters.AddWithValue("@NoOfFeeDays", dto.NoOfFeeDays.HasValue ? (object)dto.NoOfFeeDays.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@NoOfReviews", dto.NoOfReviews.HasValue ? (object)dto.NoOfReviews.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@CreatedByName", dto.CreatedByName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedById", dto.CreatedById ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@WorkstationId", dto.WorkstationId ?? (object)DBNull.Value);

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        var resultId = reader["ConsultantDoctorId"]?.ToString();
                        var isNew = Convert.ToBoolean(reader["IsNew"]);
                        return Ok(new { success = true, id = resultId, isNew });
                    }
                }
            }
            return StatusCode(500, new { success = false, message = "Unable to save" });
        }
    }
}
