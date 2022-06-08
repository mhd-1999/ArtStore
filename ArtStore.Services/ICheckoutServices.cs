using ArtStore.Models;
using System.Collections.Generic;

namespace ArtStore.Services
{
    public interface ICheckoutServices
    {
        void Checkout(Order order, List<OrderDetail> orderDetails);
    }
}
