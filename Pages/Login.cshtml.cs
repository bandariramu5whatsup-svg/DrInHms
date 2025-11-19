using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc; 
using HanuMediSoftCore.Models.Hms.Admin;
using Microsoft.AspNetCore.Http;

namespace HanuMediSoftCore.Pages
{
    public class LoginModel : BasePageModel

    {
        public LoginModel(IHttpClientFactory factory) : base(factory) { }

        public List<Users> ListUsers { get; set; } = new();

        [BindProperty] public string? Username { get; set; }
        [BindProperty] public string? Password { get; set; }


        public string? Message { get; set; }

        public void OnGet()
        {
            // empty
        }

        public async Task<IActionResult> OnPost()
        {

           


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Username!.ToString()),
                new Claim("UserId", "U0")
            };

            
            var identity = new ClaimsIdentity(claims, "CookieAuth");

            
            var principal = new ClaimsPrincipal(identity);

            
            await HttpContext.SignInAsync("CookieAuth", principal);


            var Url = "/api/Users/ValidateUserLogin";

            var request = new Users
            {
                UserName = Username,
                Password = Password
            };

            ListUsers = await PostApiAsync<List<Users>>(Url, request)
                        ?? new List<Users>();
            var user = ListUsers.FirstOrDefault();

            if (user != null)
            {

                if (user.Status == 0)
                {
                    Console.WriteLine("Inactive User");
                    return RedirectToPage("/Login");
                }

                else {
                    string? loginUserId = user.UserId;
                    string? loginUserName = user.UserName;
                    string? userType = user.UserType.ToString();
                    string? loginType = user.LoginType.ToString();

                    HttpContext.Session.SetString("gUserName", loginUserName!.ToString());
                    HttpContext.Session.GetString("gUserName");

                    HttpContext.Session.SetString("gUserId", loginUserId!);
                    HttpContext.Session.GetString("gUserId");

                    HttpContext.Session.SetString("guserType", userType!.ToString());
                    HttpContext.Session.GetString("guserType");

                    HttpContext.Session.SetString("gloginType", loginType!.ToString());
                    HttpContext.Session.GetString("gloginType");

                    HttpContext.Session.SetString("gTerminalId", Environment.MachineName);
                    HttpContext.Session.GetString("gTerminalId");


                    HttpContext.Session.SetString("gPermissions", "UNITS,OP,MASTERS");
                    Message = $"You entered Username: {Username}, Password: {Password}";
                    Console.WriteLine(loginUserId);
                    Console.WriteLine(loginUserName);
                    return RedirectToPage("/Hms/Op/Masters/Units");
                }

               

               
            }
            else
            {
                Console.WriteLine("Invalid login");
            }

            return RedirectToPage("/Hms/Op/Masters/Units");

        }
    }
}
