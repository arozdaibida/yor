using Microsoft.EntityFrameworkCore;

using yor_request_api.Application.Contracts;

namespace yor_request_api.Infrastructure.Contexts
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Concurrence> Concurrences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(x => 
            {
                x.ToTable("Users");

                x.HasKey(u => u.Id);

                x.HasIndex(u => u.Email)
                .IsUnique();

                x.HasMany(u => u.Tags)
                .WithMany(u => u.Users);
            });

            modelBuilder.Entity<Request>(x => 
            {
                x.ToTable("Requests");

                x.HasKey(u => u.Id);

                x.HasOne(u => u.Sender)
                .WithMany(u => u.SentRequests)
                .HasForeignKey(u => u.SenderId);
                
                x.HasOne(u => u.Recipient)
                .WithMany(u => u.ReceivedRequests)
                .HasForeignKey(u => u.RecipientId);
            });

            modelBuilder.Entity<Concurrence>(x => 
            {
                x.ToTable("Concurrences");

                x.HasKey(u => u.Id);
                
                x.HasOne(u => u.Sender)
                .WithMany(u => u.Concurrences)
                .HasForeignKey(u => u.SenderId);
                
                x.HasOne(u => u.Sender)
                .WithMany(u => u.Concurrences)
                .HasForeignKey(u => u.SenderId);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=ARTUR;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=yor_db");
        }
    }
}
