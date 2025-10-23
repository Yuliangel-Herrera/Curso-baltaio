using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(Category => Category.Id);

            builder.Property(Category => Category.Title)
                .IsRequired(true)
                .HasColumnType("NVARCHAR") 
                .HasMaxLength(80);

            builder.Property(Category => Category.Description)
                .IsRequired(false)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.Property(Category => Category.UserId)
                .IsRequired(true)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(160);
        }
    }
}
