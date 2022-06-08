using ArtStore.Data.Infrastructure;
using ArtStore.Models;
using ArtStore.Services.BaseServices;
using System.Collections.Generic;
using System.Linq;

namespace ArtStore.Services
{
    public class OrderDetailServices : BaseServices<OrderDetail>, IOrderDetailServices
    {
        public OrderDetailServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<OrderDetail> GetOrderDetailsByOrder(int orderId)
        {
            return _unitOfWork.OrderDetailRepository.GetQuery().Where(x => x.OrderId == orderId).ToList();
        }
    }
}
