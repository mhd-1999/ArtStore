using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtStore.Web.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }

        public string CusName { get; set; }

        public string PhoneNumber { get; set; }

        public decimal TotalPrice { get; set; }

        public string Address { get; set; }

        public int Status { get; set; }

        public DateTime CreateDate { get; set; }
    }
}