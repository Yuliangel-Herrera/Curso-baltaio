using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.HasKey(Transaction => Transaction.Id);

            builder.Property(Transaction => Transaction.Title)
                .IsRequired(true)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(Transaction => Transaction.Type)
                .IsRequired(true)
                .HasColumnType("SMALLINT");

            builder.Property(Transaction => Transaction.Amount)
                .IsRequired(true)
                .HasColumnType("MONEY");

            builder.Property(Transaction => Transaction.CreatedAT)
                .IsRequired(true); //datetime2

            builder.Property(Transaction => Transaction.PaidOrReceiveAt)
                .IsRequired(false);

            builder.Property(Transaction => Transaction.UserId)
                .IsRequired(true)
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);
        }
    }
}
