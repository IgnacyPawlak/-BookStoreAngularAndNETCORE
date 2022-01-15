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
    [Authorize]
    public class WaitingBookController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public WaitingBookController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user.UserType != UserType.Admin) return Unauthorized();

            var tab = _context.BooksList.Where(x => x.AcceptedStatus == BookStatus._waiting);
            List<BookModel> mapTab = new List<BookModel>();

            foreach (var item in tab)
            {
                mapTab.Add(new BookModel
                {
                    BookId = item.Id,
                    Title = item.Title,
                    Author = item.Author,
                    Description = item.Description
                });
            }
            return Ok(mapTab);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(int id, bool accepted)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user.UserType != UserType.Admin) return Unauthorized();

            var book = _context.BooksList.Where(x => x.Id == id).ToList();

            if (book.Count == 0) return NotFound();

            switch (accepted)
            {
                case true:
                    book.FirstOrDefault().AcceptedStatus = BookStatus._public;
                    break;
                case false:
                    book.FirstOrDefault().AcceptedStatus = BookStatus._private;
                    break;
            }
            _context.SaveChanges();
            return Ok();
        }

    }
}
