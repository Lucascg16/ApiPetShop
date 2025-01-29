using ApiPetShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Infra
{
    public class DbConnectionContext : DbContext
    {
        public DbConnectionContext(DbContextOptions<DbConnectionContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbConnectionContext).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VetServiceModel>()
                .HasMany(v => v.Vacines)
                .WithMany(c => c.VetServices)
                .UsingEntity<VetVacine>(
                "VetVacines",
                j => j.HasOne<VacineModel>().WithMany().HasForeignKey("VacineId"),
                j => j.HasOne<VetServiceModel>().WithMany().HasForeignKey("VetServiceId"));
        }

        public DbSet<UserModel> Users { get; set; } = null!;
        public DbSet<PetServiceModel> PetServices { get; set; } = null!;
        public DbSet<VetServiceModel> VetServices { get; set; } = null!;
        public DbSet<VacineModel> Vacines { get; set; } = null!;
        public DbSet<VetVacine> VetVacines { get; set; } = null!;
    }
}
