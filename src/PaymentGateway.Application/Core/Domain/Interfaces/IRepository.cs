namespace PaymentGateway.Application.Core.Domain.Interfaces
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IRepository<T> : IDisposable
    {
        T Get(Guid id);

        Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default);

        IQueryable<T> Query();

        Task InsertAsync(T entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        void Add(T entity);

        int SaveChanges();

        Task SaveAsync(CancellationToken cancellationToken = default);

    }
}