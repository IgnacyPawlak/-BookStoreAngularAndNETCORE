using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Model
{
    public class DatabaseContext : IdentityDbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=bookStore.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Node>()
                .HasKey(key => new { key.UserId, key.BookId });

            builder.Entity<Node>()
                .HasOne<User>(it => it.User)
                .WithMany(i => i.BookList)
                .HasForeignKey(x => x.UserId);

            builder.Entity<Node>()
                .HasOne<Book>(it => it.Book)
                .WithMany(i => i.UsersList)
                .HasForeignKey(x => x.BookId);
        }

        public virtual DbSet<User> UsersList { get; set; }

        public virtual DbSet<Book> BooksList { get; set; }

        public virtual DbSet<Node> NodesList { get; set; }
    }
}
