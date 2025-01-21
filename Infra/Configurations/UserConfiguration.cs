using ApiPetShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPetShop.Infra.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Role)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.IsDeleted);
            builder.Property(x => x.CreatedDate).HasColumnType("dateTime");
            builder.Property(x => x.UpdatedDate).HasColumnType("dateTime");
        }
    }
}
