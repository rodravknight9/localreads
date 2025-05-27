using LocalReads.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace LocalReads.API.Context
{
    public class LocalReadsContext : DbContext
    {
        static LocalReadsContext()
        {
            Batteries.Init();
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<User> Users { get; set; }

        public LocalReadsContext(DbContextOptions<LocalReadsContext> options)
            : base(options)
        {
        }
    }
}
