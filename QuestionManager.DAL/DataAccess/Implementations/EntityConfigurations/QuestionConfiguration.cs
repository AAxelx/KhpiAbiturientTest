using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionManager.DAL.DataAccess.Implementations.Entities;

namespace QuestionManager.DAL.DataAccess.Implementations.EntityConfigurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<QuestionEntity>
    {
        public void Configure(EntityTypeBuilder<QuestionEntity> builder)
        {
            builder.ToTable("Question").HasKey(p => p.Id);
            builder.Property(p => p.Question).IsRequired().HasColumnName("Question").HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(p => p.Answear).IsRequired().HasColumnName("Answear").HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(p => p.Complexity).IsRequired().HasColumnName("Complexity").HasColumnType("int");
            builder.Property(p => p.SecondOption).IsRequired().HasColumnName("SecondOption").HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(p => p.ThirdOption).IsRequired().HasColumnName("ThirdOption").HasColumnType("nvarchar").HasMaxLength(100);
        }
    }
}
