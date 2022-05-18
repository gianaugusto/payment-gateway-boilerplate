namespace PaymentGateway.Data.SQL.Mappings
{
    using Application.Features.PaymentProcessor.Domain.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PaymentMap : IEntityTypeConfiguration<Payment>
    {
		void IEntityTypeConfiguration<Payment>.Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable(Payment.TableName);

            builder.HasKey(x => x.PaymentId);
            builder.Property(x => x.PaymentId)
                .HasDefaultValueSql("NEWID()")
                .IsRequired();
            builder.Property(x => x.MerchantId).IsRequired();
            builder.Property(x => x.SourceId).IsRequired();
            builder.Property(x => x.Description);

            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.Currency).IsRequired();
            builder.Property(x => x.PaymentType).IsRequired();
            builder.Property(x => x.Reference).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            
            builder
                .HasOne(c => c.Merchant)
                .WithOne()
                .HasForeignKey<Payment>(a => a.MerchantId);
            
            builder
                .HasOne(c => c.Source)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<Payment>(a => a.SourceId);

        }

    }
}
