using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionManager.DAL.DataAccess.Implementations.Entities;

namespace QuestionManager.DAL.DataAccess.Implementations.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User").HasKey(p => p.Id);
            builder.Property(p => p.Email).IsRequired().HasColumnName("Email").HasColumnType("varchar").HasMaxLength(40);
            builder.Property(p => p.Score).IsRequired().HasColumnName("Score").HasColumnType("int");
        }
    }
}
