using ArtStore.Models;
using ArtStore.Services;
using ArtStore.Web.Areas.Admin.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ArtStore.Web.Areas.Admin.Controllers
{
    public class OrderManagementController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly IArtServices _artServices;
        private readonly IOrderDetailServices _orderDetailServices;

        public OrderManagementController(IOrderServices orderServices, IArtServices artServices, IOrderDetailServices orderDetailServices)
        {
            _orderServices = orderServices;
            _artServices = artServices;
            _orderDetailServices = orderDetailServices;
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

        // GET: Admin/OrderManagement
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string search, int? pageIndex = 1, int pageSize = 10)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;

            if (search != null)
            {
                pageIndex = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewData["CurrentFilter"] = search;

            Expression<Func<Order, bool>> filter = null;

            if (!String.IsNullOrEmpty(search))
            {
                filter = c => c.Address.Contains(search);
            }

            Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null;

            switch (sortOrder)
            {
                default:
                    orderBy = n => n.OrderByDescending(p => p.Id);
                    break;
            }

            var orders = await _orderServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(orders);
        }

        // GET: Admin/OrderManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _orderServices.GetById((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }

            var orderViewModel = new OrderViewModel
            {
                Id = order.Id,
                Address = order.Address,
                CreateDate = order.CreateDate,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                CusName = UserManager.FindById(order.AccountId).UserName
            };
            return View(orderViewModel);
        }

        public ActionResult GetOrderDetails(int orderId)
        {
            var list = _orderDetailServices.GetOrderDetailsByOrder(orderId);
            return PartialView("GetOrderDetails", list);
        }

        public string GetAccountName(string accId)
        {
            var user = UserManager.FindById(accId);
            return user.Name;
        }

        public string GetArtName(int artId)
        {
            var name = _artServices.GetById(artId).Name;
            return name;
        }

        // POST: Admin/OrderManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var order = _orderServices.GetById(orderViewModel.Id);
                if(order == null)
                {
                    return HttpNotFound();
                }
                orderViewModel.Address = order.Address;
                
                if(orderViewModel.Status == 0)
                {
                    order.Status = 1;
                }

                var result = _orderServices.Update(order);
                if (result)
                {
                    TempData["Message"] = "Cập nhật thành công.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Cập nhật thất bại.";
                }
            }
            return View(orderViewModel);
        }
    }
}
