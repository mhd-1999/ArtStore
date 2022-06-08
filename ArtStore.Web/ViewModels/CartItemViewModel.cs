using ArtStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtStore.Web.ViewModels
{
    public class CartItemViewModel
    {
        public Art Art { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}