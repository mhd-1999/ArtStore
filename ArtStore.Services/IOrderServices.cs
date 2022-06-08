using ArtStore.Models;
using ArtStore.Services.BaseServices;
using System.Collections.Generic;

namespace ArtStore.Services
{
    public interface IOrderServices : IBaseService<Order>
    {
        IEnumerable<Order> GetOrderHistory(string accId);
    }
}
