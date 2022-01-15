using BookStoreApi.ConnectModel;
using BookStoreApi.Model;
using Microsoft.AspNetCore.Authorization;
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
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public UserController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        [HttpGet("ALL")]
        public IActionResult Get()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user.UserType != UserType.Admin) return Unauthorized();

            var tab = _context.UsersList.ToList();
            List<UserModel> mapTab = new List<UserModel>();

            foreach (var item in tab)
            {
                mapTab.Add(new UserModel
                {
                    Id = item.Id,
                    Email = item.Email,
                    FullName = item.UserFullName,
                    IsAdmin = item.UserType == UserType.Admin ? true : false
                });
            }
            return Ok(mapTab);
        }

        [HttpGet]
        public IActionResult Get(string id)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user.UserType != UserType.Admin) return Unauthorized();

            var buff = _context.UsersList.Where(x => x.Id == id).ToList();
            if (buff.Count == 0) return NotFound();

            var buffUser = buff.FirstOrDefault();

            return Ok(new UserModel
            {
                Id = buffUser.Id,
                Email = buffUser.Email,
                FullName = buffUser.UserFullName,
                IsAdmin = buffUser.UserType == UserType.Admin ? true : false
            });
        }
        

        [HttpPatch]
        public IActionResult Patch([FromBody] PatchUserModel input)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;

            switch (user.UserType)
            {
                case UserType.NoAuthorize:
                    return Unauthorized();
                //break;
                case UserType.Normal:
                    user.UserFullName = input.FullName;
                    //_userManager.
                    break;
                case UserType.Admin:
                    //_context.
                    break;
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            List<User> buff = new List<User>();

            switch (user.UserType)
            {
                case UserType.NoAuthorize:
                    return Unauthorized();
                //break;
                case UserType.Normal:
                    buff = _context.UsersList.Where(x => x.Id == id && x.UserType != UserType.Admin).ToList();
                    break;
                case UserType.Admin:
                    buff = _context.UsersList.Where(x => x.Id == id).ToList();
                    break;
            }

            if (buff.Count == 0) return NotFound();
            _context.UsersList.Remove(buff.FirstOrDefault());
            _context.SaveChanges();
            return Ok();
        }
    }
}
