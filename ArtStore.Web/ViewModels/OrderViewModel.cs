using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtStore.Web.ViewModels
{
    public class OrderViewModel
    {
        public string CusName { get; set; }

        public string PhoneNum { get; set; }

        public decimal TotalPrice { get; set; }

        public string Address { get; set; }

        public string NumberCard { get; set; }

        public string Cvv { get; set; }

        public string Email { get; set; }
    }
}