using BusinessLogicLayer.IServices;
using BusinessModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeNexus.Controllers
{
    public class Login : Controller
    {
        private readonly ILoginService _ILoginService;
        private readonly IConfiguration _config;
        public Login(ILoginService iLoginService, IConfiguration config)
        {
            _ILoginService = iLoginService;
            _config = config;
        }


        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(EmployeeProfileDetails loginCredentials)
        {
            // Validate user credentials
            EmployeeProfileDetails login = _ILoginService.LoginAuthenticationService(loginCredentials.LoginName, loginCredentials.Password);

            if (login == null)
            {
                return RedirectToAction("UserLogin");
            }

            // Generate JWT token
            var token = GenerateJwtToken(login);

            // Store token in session or return to client
            HttpContext.Session.SetString("JWTToken", token);

            // Store user details 
            var loginJson = JsonConvert.SerializeObject(login);
            HttpContext.Session.SetString("UserDetails", loginJson);

            return RedirectToAction("Index", "Dashboard");
        }

        private string GenerateJwtToken(EmployeeProfileDetails login)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, login.LoginName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
