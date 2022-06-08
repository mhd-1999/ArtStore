using ArtStore.Services;
using ArtStore.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtStore.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IArtServices _artServices;

        public CartController(IArtServices artServices)
        {
            _artServices = artServices;
        }

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session["cart"] as List<CartItemViewModel> ?? new List<CartItemViewModel>();

            if (cart.Count == 0 || Session["cart"] == null)
            {
                ViewBag.Message = "There are no items in your bag.";
                return View();
            }

            decimal totalPrice = 0m;

            foreach (var item in cart)
            {
                totalPrice += item.Price * item.Quantity;
            }

            ViewBag.TotalPrice = totalPrice;

            return View(cart);
        }

        public int CartNoti()
        {
            var cart = Session["cart"] as List<CartItemViewModel> ?? new List<CartItemViewModel>();

            return cart.Count;
        }

        public ActionResult AddToCart(int artId)
        {
            var art = _artServices.GetById(artId);
            if (art == null)
            {
                return HttpNotFound();
            }
            List<CartItemViewModel> cart = (List<CartItemViewModel>)Session["cart"];
            if (cart == null)
            {
                cart = new List<CartItemViewModel>();
            }
            foreach (var cartItem in cart)
            {
                if (cartItem.Art.Id == artId)
                {
                    cartItem.Quantity++;
                    Session["cart"] = cart;
                    return RedirectToAction("Index", "Home");
                }
            }
            CartItemViewModel item = new CartItemViewModel()
            {
                Art = art,
                Quantity = 1,
                Price = art.Price
            };
            cart.Add(item);

            Session["cart"] = cart;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Increment(int itemIndex)
        {
            List<CartItemViewModel> cart = (List<CartItemViewModel>)Session["cart"];
            cart.ElementAt(itemIndex).Quantity++;
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        //GET: Cart/Decrement/itemIndex
        public ActionResult Decrement(int itemIndex)
        {
            List<CartItemViewModel> cart = (List<CartItemViewModel>)Session["cart"];
            if (cart.ElementAt(itemIndex).Quantity == 1)
            {
                return RemoveItem(itemIndex);
            }
            else
            {
                cart.ElementAt(itemIndex).Quantity--;
            }
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        //GET: Cart/RemoveItem/itemIndex
        public ActionResult RemoveItem(int itemIndex)
        {
            List<CartItemViewModel> cart = (List<CartItemViewModel>)Session["cart"];
            cart.RemoveAt(itemIndex);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
    }
}