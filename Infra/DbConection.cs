using ApiPetShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Infra
{
    public class DbConection : DbContext
    {
        public DbConection(DbContextOptions<DbConection> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbConection).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VetServiceModel>()
                .HasMany(v => v.Vacines)
                .WithMany(c => c.VetServices)
                .UsingEntity<Dictionary<string, object>>(
                "VetVacines",
                j => j.HasOne<VacineModel>().WithMany().HasForeignKey("VacineId"),
                j => j.HasOne<VetServiceModel>().WithMany().HasForeignKey("VetServiceId"));
        }

        public DbSet<UserModel> Users { get; set; } = null!;
        public DbSet<PetServiceModel> PetServices { get; set; } = null!;
        public DbSet<VetServiceModel> VetServices { get; set; } = null!;
        public DbSet<VacineModel> Vacines { get; set; } = null!;
    }
}
