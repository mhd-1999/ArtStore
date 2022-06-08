using ArtStore.Data.Infrastructure;
using ArtStore.Data.Infrastructure.Repositories;
using ArtStore.Models;
using System;
using System.Collections.Generic;

namespace ArtStore.Services
{
    public class CheckoutServices : ICheckoutServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICoreRepository<Order> _orderRepository;
        private readonly ICoreRepository<OrderDetail> _orderDetailRepository;

        public CheckoutServices(IUnitOfWork unitOfWork, ICoreRepository<Order> orderRepository, ICoreRepository<OrderDetail> orderDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public void Checkout(Order order, List<OrderDetail> orderDetails)
        {
            order.CreateDate = DateTime.Now;
            _orderRepository.Add(order);

            foreach (var item in orderDetails)
            {
                item.Order = order;
                _orderDetailRepository.Add(item);
            }

            _unitOfWork.SaveChanges();
        }
    }
}
