using ArtStore.Models;
using ArtStore.Services;
using ArtStore.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtStore.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly ICheckoutServices _checkoutServices;
        private readonly IOrderDetailServices _orderDetailServices;
        private readonly IArtServices _artServices;

        public OrdersController(IOrderServices orderServices, ICheckoutServices checkoutServices, 
            IOrderDetailServices orderDetailServices, IArtServices artServices)
        {
            _orderServices = orderServices;
            _checkoutServices = checkoutServices;
            _orderDetailServices = orderDetailServices;
            _artServices = artServices;
        }
        private AccountManager _userManager;
        public AccountManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AccountManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Orders
        public ActionResult Checkout()
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = UserManager.FindById(userId);

                var orderViewModel = new OrderViewModel
                {
                    CusName = user.UserName,
                    Address = user.Address,
                    PhoneNum = user.PhoneNumber,
                    Cvv = user.Cvv,
                    NumberCard = user.NumberCard,
                    Email = user.Email
                };

                return View(orderViewModel);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(OrderViewModel model)
        {
            var cart = Session["cart"];
            var cartItems = (List<CartItemViewModel>)cart;

            if(cartItems.Count == 0)
            {
                return View("Index", "Cart");
            }

            decimal TotalPrice = 0;

            if (ModelState.IsValid)
            {
                var orderDetails = new List<OrderDetail>();
                foreach (var item in cartItems)
                {
                    var orderDetail = new OrderDetail
                    {
                        Quantity = item.Quantity,
                        ArtId = item.Art.Id,
                        Price = item.Price * item.Quantity
                    };

                    TotalPrice += item.Price * item.Quantity;
                    orderDetails.Add(orderDetail);
                }

                var order = new Order
                {
                    AccountId = User.Identity.GetUserId(),
                    TotalPrice= TotalPrice,
                    Address = model.Address,
                    Status = 0
                };

                _checkoutServices.Checkout(order, orderDetails);

                cartItems.Clear();
                return RedirectToAction("CheckoutSuccess");
            }
            return View(model);
        }

        public ActionResult OrderHistory(int? pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentPageSize"] = pageSize;

            var userId = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return HttpNotFound();
            }
            var orders = _orderServices.GetOrderHistory(userId);
            int pageNum = pageIndex ?? 1;

            return View(orders.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Details(int id)
        {
            var order = _orderServices.GetById(id);
            var user = UserManager.FindById(order.AccountId);

            var orderDetailViewModel = new OrderDetailViewModel
            {
                Id = order.Id,
                Address = order.Address,
                CusName = user.Name,
                Status = order.Status,
                CreateDate = order.CreateDate,
                PhoneNumber = user.PhoneNumber,
                TotalPrice = order.TotalPrice
            };
            return View(orderDetailViewModel);
        }

        public ActionResult GetOrderDetails(int orderId)
        {
            var list = _orderDetailServices.GetOrderDetailsByOrder(orderId);
            return PartialView("_GetOrderDetails", list);
        }

        public string GetArtName(int artId)
        {
            var name = _artServices.GetById(artId).Name;
            return name;
        }

        public ActionResult CheckoutSuccess()
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            ViewBag.CheckoutSuccess = user.UserName + ", cảm ơn bạn đã đặt hàng, đơn hàng của bạn sẽ được ship nhanh nhất.";
            return View();
        }
    }
}