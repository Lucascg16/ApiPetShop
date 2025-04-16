using ApiPetShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPetShop.Infra.Configurations
{
    public class VetServiceConfiguration : IEntityTypeConfiguration<VetServiceModel>
    {
        public void Configure(EntityTypeBuilder<VetServiceModel> builder)
        {
            builder.ToTable("VetServices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Email)
                .IsRequired(false);

            builder.Property(x => x.PhoneNumber)
                .IsRequired(false);

            builder.Property(x => x.IsWhatsApp)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.PetName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.PetAge)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(x => x.PetType)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.PetGender)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.PetSize)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.ScheduledDate)
                .IsRequired()
                .HasColumnType("dateTime");

            builder.Property(x => x.PetWeight)
                .IsRequired();

            builder.Property(x => x.IsCastrated)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.IsDeleted);
            builder.Property(x => x.CreatedDate).HasColumnType("dateTime");
            builder.Property(x => x.UpdatedDate).HasColumnType("dateTime");
        }
    }
}
