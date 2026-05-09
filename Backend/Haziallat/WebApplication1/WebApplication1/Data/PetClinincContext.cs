using Microsoft.EntityFrameworkCore;
using PetClinicApi.Entities;
using System.Security.Cryptography.X509Certificates;

namespace PetClinicApi.Data
{
    public class PetClinicContext : DbContext
    {
        public PetClinicContext(DbContextOptions<PetClinicContext> options) : base(options)
        {

        }

        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Pet> Pets => Set<Pet>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Owner>(e =>
            {
                e.Property(o => o.FirstName).HasMaxLength(60).IsRequired();
                e.Property(o => o.LastName).HasMaxLength(60).IsRequired();
                e.Property(o => o.Phone).HasMaxLength(30);
                e.Property(o => o.Email).HasMaxLength(120);
            });

            mb.Entity<Pet>(e =>
            {
                e.Property(p => p.Name).HasMaxLength(60).IsRequired();
                e.Property(p => p.Species).HasMaxLength(20).IsRequired();
                e.Property(p => p.Breed).HasMaxLength(80);
                e.HasOne(p => p.Owner)
                    .WithMany(o => o.Pets)
                    .HasForeignKey(p => p.OwnerId);
            });

            mb.Entity<Owner>().HasData(
                new Owner { Id = 1, FirstName = "Kovacs", LastName = "Anna", Phone = "+36 20 123 4567", Email = "annakovacs@gmail.com" },
                new Owner { Id = 2, FirstName = "Nagy", LastName = "Béla", Phone = "+36 30 987 6543", Email = null },
                new Owner { Id = 3, FirstName = "Horváth", LastName = "Csilla", Phone = null, Email = "csilla.h@freemail.hu" }
                );

            mb.Entity<Pet>().HasData(
            new Pet
            {
                Id = 1,
                Name = "Bodri",
                Species = "Kutya",
                Breed = "Labrador",
                BirthYear = 2019,
                OwnerId = 1
            },
            new Pet
            {
                Id = 2,
                Name = "Cirmos",
                Species = "Macska",
                Breed = "Európai rövidszőrű",
                BirthYear = 2021,
                OwnerId = 1
            },
            new Pet
            {
                Id = 3,
                Name = "Rex",
                Species = "Kutya",
                Breed = "Németjuhász",
                BirthYear = 2020,
                OwnerId = 2
            },
            new Pet
            {
                Id = 4,
                Name = "Hópihe",
                Species = "Nyúl",
                Breed = null,
                BirthYear = 2022,
                OwnerId = 3
            },
            new Pet
            {
                Id = 5,
                Name = "Mici",
                Species = "Macska",
                Breed = "Perzsa",
                BirthYear = 2018,
                OwnerId = 3
            }
            );
        }
    }
}
