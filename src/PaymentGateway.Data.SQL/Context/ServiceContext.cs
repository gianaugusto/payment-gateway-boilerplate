namespace PaymentGateway.Data.SQL.Context
{
    using Microsoft.EntityFrameworkCore;

    using PaymentGateway.Data.SQL.Mappings;

    public class ServiceContext : DbContext
    {
        public ServiceContext() { }

        public ServiceContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PaymentMap());
            modelBuilder.ApplyConfiguration(new MerchantMap());
            modelBuilder.ApplyConfiguration(new SourceMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            
        }
    }
}
