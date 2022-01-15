using BookStoreApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyBookController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MyBookController(DatabaseContext context)
        {
            _context = context;
        }
    }
}
