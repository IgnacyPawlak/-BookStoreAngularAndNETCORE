using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Model
{
    public class BookStoreUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
