using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmailManager.DAL.DataAccess.Implementations.Entities;

namespace EmailManager.DAL.DataAccess.Implementations.EntityConfigurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<QuestionEntity>
    {
        public void Configure(EntityTypeBuilder<QuestionEntity> builder)
        {
            builder.ToTable("Question").HasKey(p => p.Id);
            builder.Property(p => p.Question).IsRequired().HasColumnName("Question").HasColumnType("varchar").HasMaxLength(100);
            builder.Property(p => p.Correct).IsRequired().HasColumnName("Correct").HasColumnType("bit");
            builder.Property(p => p.Complexity).IsRequired().HasColumnName("Complexity").HasColumnType("int");
        }
    }
}
