using BookStoreApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreUserController : ControllerBase
    {
        private UserManager<BookStoreUser> _userManager;
        private SignInManager<BookStoreUser> _signInManager;

        public BookStoreUserController(UserManager<BookStoreUser> userManager, SignInManager<BookStoreUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/BookStoreUser/Register
        public async Task<Object> PostBookStoreUser(BookStoreUserModel model)
        {
            var bookStoreUser = new BookStoreUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };
            try
            {
                var result = await _userManager.CreateAsync(bookStoreUser, model.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
