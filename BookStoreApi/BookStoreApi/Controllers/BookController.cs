using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApi.Model;
using Microsoft.AspNetCore.Authorization;
using BookStoreApi.ConnectModel;
using Microsoft.AspNetCore.Identity;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public BookController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Book
        [HttpGet]
        public IActionResult Get()
        {
            var tab = _context.BooksList.Where(x => x.AcceptedStatus == BookStatus._public);
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

        [HttpPatch]
        public IActionResult Patch([FromBody] BookModel input)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user.UserType != UserType.Admin) return Unauthorized();

            var book = _context.BooksList.Where(x => x.Id == input.BookId && x.AcceptedStatus == BookStatus._public).ToList();

            if (book.Count == 0) return BadRequest();

            book.FirstOrDefault().Title = input.Title;
            book.FirstOrDefault().Author = input.Author;
            book.FirstOrDefault().Description = input.Description;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            List<Book> buff = new List<Book>();

            switch (user.UserType)
            {
                case UserType.NoAuthorize:
                    return Unauthorized();
                    break;
                case UserType.Normal:
                    buff = _context.BooksList.Where(x => x.Id == id && x.AcceptedStatus != BookStatus._public).ToList();
                    break;
                case UserType.Admin:
                    buff = _context.BooksList.Where(x => x.Id == id).ToList();
                    break;
            }

            if (buff.Count == 0) return BadRequest();
            _context.BooksList.Remove(buff.FirstOrDefault());
            _context.SaveChanges();
            return Ok();
        }

    }
}
