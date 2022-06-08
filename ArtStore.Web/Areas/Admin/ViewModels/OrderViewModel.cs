using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtStore.Web.Areas.Admin.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public string CusName { get; set; }
    }
}