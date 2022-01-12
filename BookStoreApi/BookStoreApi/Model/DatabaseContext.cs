using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Model
{
    public class DatabaseContext: IdentityDbContext
    {
        public DatabaseContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=bookStore.db");
        }

        public virtual DbSet<BookStoreUser> BookStoreUsers { get; set; }

        public virtual DbSet<Book> Books { get; set; }
    }
}
