using ArtStore.Models.BaseEntities;
using System;
using System.Collections.Generic;

namespace ArtStore.Models
{
    public class Order : BaseEntity
    {
        public DateTime CreateDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public string AccountId { get; set; }
        public Account Account { get; set; }
        public virtual ICollection<OrderDetail> Details { get; set; }
    }
}