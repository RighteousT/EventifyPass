using EventifyPass.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EventifyPass.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Music" },
                new Category { CategoryId = 2, Name = "Business" },
                new Category { CategoryId = 3, Name = "Sports" },
                new Category { CategoryId = 4, Name = "Arts" }

            );
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}

