namespace PaymentGateway.Data.SQL.Repositories
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Core.Domain.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public TEntity Get(Guid id)
        {
            return Db.Find<TEntity>(id);
        }

        public async Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await Db.FindAsync<TEntity>(id);
        }

        public IQueryable<TEntity> Query()
        {
            return Db.Set<TEntity>().AsQueryable();
        }

        public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Db.Set<TEntity>().Add(entity);
            await Db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Db.Entry(entity).State = EntityState.Modified;
            await Db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await Db.Set<TEntity>().FindAsync(id);

            Db.Entry(entity).State = EntityState.Deleted;
            await Db.SaveChangesAsync(cancellationToken);
        }

        public void Add(TEntity entity)
        {
            Db.Set<TEntity>().Add(entity);
        }

        public int SaveChanges() =>
            Db.SaveChanges();

        public Task SaveAsync(CancellationToken cancellationToken = default) =>
            Db.SaveChangesAsync(cancellationToken);

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
