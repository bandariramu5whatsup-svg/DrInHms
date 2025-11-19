using HanuMediSoftCore.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace CoreRazorDemo.Pages
{
    public class LoginNewModel : PageModel
    {

        private readonly string? _conn;

        public LoginNewModel(IConfiguration config)
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
            using SqlConnection con = new SqlConnection(_conn);
            con.Open();

            string sql = @"SELECT PasswordHash, Salt, Iterations 
                       FROM Users WHERE UserName=@u";

            using SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@u", UserName);

            using SqlDataReader r = cmd.ExecuteReader();

            if (!r.Read())
            {
                Error = "Invalid username or password";
                return;
            }

            byte[] dbHash = (byte[])r["PasswordHash"];
            byte[] dbSalt = (byte[])r["Salt"];
            int iterations = (int)r["Iterations"];

            bool ok = PasswordHasher.VerifyPassword(Password!, dbHash, dbSalt, iterations);

            if (ok)
                Message = "Login Successful!";
            else
                Error = "Invalid username or password";
        }
    }
}