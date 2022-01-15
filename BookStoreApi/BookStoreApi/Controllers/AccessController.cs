using BookStoreApi.ConnectModel;
using BookStoreApi.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccessController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public AccessController(IConfiguration config, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginUserModel loginModel)
        {
            IActionResult res = Unauthorized();
            var success = AuthenticateUser(loginModel);

            if (success)
            {
                var tokenString = GenerateJsonWebToken(loginModel);
                res = Ok(new { token = tokenString });
            }
            return res;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Registration([FromBody] RegistrationUserModel InputModel)
        {
            IActionResult res = BadRequest();

            var user = new User { UserName = InputModel.Email,  
                                  Email = InputModel.Email,
                                  UserFullName = InputModel.FullName,
                                  UserType = UserType.Normal };

            user.EmailConfirmed = true; // testing

            var result = _userManager.CreateAsync(user, InputModel.Password).Result;

            if (result.Succeeded)
            {
                // Email do napisania
                

                res = Ok();
            }
            return res;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult EmailVerification()
        {
            return NotFound();
        }

        [HttpGet]
        [Authorize]
        public IActionResult MyUser()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            return Ok(new UserModel 
            { 
                Id = user.Id,
                Email = user.Email,
                FullName = user.UserFullName,
                IsAdmin = user.UserType == UserType.Admin ? true : false
            });
        }


        private string GenerateJsonWebToken(LoginUserModel loginModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                                             _config["Jwt:Issuer"],
                                             null /*role do zrobienia*/,
                                             expires: DateTime.Now.AddMinutes(30),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool AuthenticateUser(LoginUserModel loginModel)
        {
            var result = _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, lockoutOnFailure : false).Result;

            return result.Succeeded;
        }
    }
}
