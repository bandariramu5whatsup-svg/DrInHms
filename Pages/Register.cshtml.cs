using HanuMediSoftCore.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace HanuMediSoftCore.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly string? _conn;

        public RegisterModel(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        [BindProperty]
        public string? UserName { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        public string? Message { get; set; }
        public string? Error { get; set; }

        public void OnGet() { }

        public void OnPost()
        {
            try
            {
                var (hash, salt, iterations) = PasswordHasher.HashPassword(Password!);

                using SqlConnection con = new SqlConnection(_conn);
                con.Open();

                string sql = @"INSERT INTO Users (UserName, PasswordHash, Salt, Iterations)
                           VALUES (@u, @h, @s, @i)";

                using SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@u", UserName);
                cmd.Parameters.AddWithValue("@h", hash);
                cmd.Parameters.AddWithValue("@s", salt);
                cmd.Parameters.AddWithValue("@i", iterations);

                cmd.ExecuteNonQuery();

                Message = "User registered successfully!";
            }
            catch (SqlException ex)
            {
                Error = ex.Message;
            }
        }
    }
}