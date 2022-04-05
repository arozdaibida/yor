using Microsoft.EntityFrameworkCore;

using yor_search_api.Models;

namespace yor_search_api.Infrastructure.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public DbSet<Tag> Tag { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("Users");

                u.HasKey(x => x.Id);

                u.HasIndex(x => x.Email)
                .IsUnique();

                u.HasMany(x => x.Tags)
                .WithMany(x => x.Users);
            });

            modelBuilder.Entity<Tag>(u => 
            {
                u.ToTable("Tags");

                u.HasKey(x => x.Id);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=ARTUR;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=yor_db");
        }
    }
}
