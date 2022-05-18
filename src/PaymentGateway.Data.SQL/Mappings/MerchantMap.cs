namespace PaymentGateway.Data.SQL.Mappings
{
    using Application.Features.PaymentProcessor.Domain.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MerchantMap : IEntityTypeConfiguration<Merchant>
    {
		void IEntityTypeConfiguration<Merchant>.Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.ToTable(Merchant.TableName);

            builder.HasKey(x => x.MerchantId);
            builder.Property(x => x.MerchantId)
                .HasDefaultValueSql("NEWID()")
                .IsRequired();
            builder.Property(x => x.MerchantName).IsRequired();
            builder.Property(x => x.PrivateKey).IsRequired();
            builder.Property(x => x.PublicKey).IsRequired();
            builder.Property(x => x.SuccessUrl).IsRequired();
            builder.Property(x => x.FailureUrl).IsRequired();
        }

    }
}
