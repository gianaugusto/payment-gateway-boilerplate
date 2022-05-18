namespace PaymentGateway.Data.SQL.Tools
{
    using System;

    using Microsoft.EntityFrameworkCore;

    using PaymentGateway.Application.Features.PaymentProcessor.Domain.Models;

    public class Seed
    {
        private readonly DbContext _context;

        public Seed(DbContext context)
        {
            this._context = context;
        }

        public void SeedServiceData()
        {
            try
            {
                AddMerchants();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        private void AddMerchants()
        {
            var merchants = new List<Merchant>()
            {
                new Merchant(Guid.Parse("E659D58B-9EF6-4E78-8EB0-71E7AE88A701"), "Store 1","http://store1.com/success?id=","http://store1.com/fail?id=","private-key","public-key"),
                new Merchant(Guid.Parse("E659D58B-9EF6-4E78-8EB0-71E7AE88A702"), "Store 2","http://store2.com/success?id=","http://store2.com/fail?id=","private-key","public-key"),
                new Merchant(Guid.Parse("E659D58B-9EF6-4E78-8EB0-71E7AE88A703"), "Store 3","http://store3.com/success?id=","http://store3.com/fail?id=","private-key","public-key"),
            };
            
            foreach (var merchant in merchants)
            {
                _context.Add<Merchant>(merchant);
            }

            _context.SaveChanges();
        }
    }

}
