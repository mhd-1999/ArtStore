using ArtStore.Models;
using ArtStore.Services.BaseServices;
using System.Collections.Generic;

namespace ArtStore.Services
{
    public interface IOrderDetailServices : IBaseService<OrderDetail>
    {
        IEnumerable<OrderDetail> GetOrderDetailsByOrder(int orderId);
    }
}
