namespace PaymentGateway.Application.Core.Domain.Model
{
    using System;
    
    public abstract class BaseEntity
    {
        public BaseEntity(string name)
        {
            this.Name = name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

    }
}