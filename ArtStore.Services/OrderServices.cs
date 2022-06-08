using ArtStore.Data.Infrastructure;
using ArtStore.Models;
using ArtStore.Services.BaseServices;
using System.Collections.Generic;
using System.Linq;

namespace ArtStore.Services
{
    public class OrderServices : BaseServices<Order>, IOrderServices
    {
        public OrderServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Order> GetOrderHistory(string accId)
        {
            return _unitOfWork.OrderRepository.GetQuery().Where(x => x.AccountId == accId).OrderByDescending(d => d.CreateDate).ToList();
        }
    }
}
