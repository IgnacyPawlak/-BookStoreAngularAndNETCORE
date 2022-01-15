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
    [Authorize]
    public class MyBookController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public MyBookController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user.UserType == UserType.NoAuthorize) return Unauthorized();
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post()
        {
            return Ok();
        }

        [HttpPatch]
        [Authorize]
        public IActionResult Patch()
        {
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}
