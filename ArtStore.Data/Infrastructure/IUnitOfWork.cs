using ArtStore.Data.Infrastructure.Repositories;
using ArtStore.Models;
using ArtStore.Models.BaseEntities;
using System;
using System.Threading.Tasks;

namespace ArtStore.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        ArtStoreContext DataContext { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        ICoreRepository<T> CoreRepository<T>() where T : BaseEntity;

        #region Master Data

        ICoreRepository<Category> CategoryRepository { get; }

        ICoreRepository<OrderDetail> OrderDetailRepository { get; }

        ICoreRepository<Art> ArtRepository { get; }

        ICoreRepository<Order> OrderRepository { get; }

        #endregion

    }
}
