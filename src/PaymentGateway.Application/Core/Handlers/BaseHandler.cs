namespace PaymentGateway.Application.Core.Handlers
{
    using System;
    using System.Linq.Expressions;

    using PaymentGateway.Application.Core.Domain.Model;

    public class BaseHandler
    {
        public Expression<Func<T, bool>> FilterByName<T>(string filter) where T : BaseEntity
        {
            if (string.IsNullOrEmpty(filter))
            {
                return default;
            }

            return x => x.Name.Contains(filter);
        }
    }
}
