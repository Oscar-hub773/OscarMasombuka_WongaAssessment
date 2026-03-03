using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WongaAssessment.API.Models.Domain;

namespace WongaAssessment.API.Data.Configurations
{
    public class UserConfiguration
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.UserId);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property(x => x.CreatedDate)
                .IsRequired();
        }
    }
}
