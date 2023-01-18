using Microsoft.EntityFrameworkCore;
using PetsDB.Models;

namespace PetsDB
{
    public class PetsContext : DbContext
    {
        public PetsContext(DbContextOptions<PetsContext> options) : base(options) 
        {

        }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>().ToTable("Owner");
            modelBuilder.Entity<Pet>().ToTable("Pet");
        }

    }
}