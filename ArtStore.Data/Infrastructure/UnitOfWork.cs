using ArtStore.Data;
using ArtStore.Data.Infrastructure;
using ArtStore.Data.Infrastructure.Repositories;
using ArtStore.Models;
using ArtStore.Models.BaseEntities;
using System.Threading.Tasks;

namespace Res247.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArtStoreContext _dbContext;
        public ArtStoreContext DataContext => _dbContext;

        public UnitOfWork(ArtStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        private ICoreRepository<Category> _categoryRepository;
        public ICoreRepository<Category> CategoryRepository => _categoryRepository ?? new CoreRepository<Category>(_dbContext);

        private ICoreRepository<OrderDetail> _orderDetailRepository;
        public ICoreRepository<OrderDetail> OrderDetailRepository => _orderDetailRepository?? new CoreRepository<OrderDetail>(_dbContext);

        private ICoreRepository<Art> _artRepository;
        public ICoreRepository<Art> ArtRepository => _artRepository ?? new CoreRepository<Art>(_dbContext);

        private ICoreRepository<Order> _orderRepository;
        public ICoreRepository<Order> OrderRepository => _orderRepository ?? new CoreRepository<Order>(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public ICoreRepository<T> CoreRepository<T>() where T : BaseEntity
        {
            return new CoreRepository<T>(_dbContext);
        }
    }
}
