namespace PaymentGateway.Data.SQL.Mappings
{
    using Application.Features.PaymentProcessor.Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SourceMap : IEntityTypeConfiguration<Source>
    {
		void IEntityTypeConfiguration<Source>.Configure(EntityTypeBuilder<Source> builder)
        {
            builder.ToTable(Source.TableName);

            builder.HasKey(x => x.SourceId);
            builder.Property(x => x.SourceId)
                .HasDefaultValueSql("NEWID()")
                .IsRequired();
            builder.Property(x => x.BillingAddress).IsRequired();
            builder.Property(x => x.ExpiryMonth).IsRequired();
            builder.Property(x => x.ExpiryYear).IsRequired();
            builder.Property(x => x.Issuer).IsRequired();
            builder.Property(x => x.Fingerprint);
            builder.Property(x => x.Last4);
        }

    }
}
