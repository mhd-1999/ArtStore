using ArtStore.Models.BaseEntities;

namespace ArtStore.Models
{
    public class OrderDetail : BaseEntity
    {
        public decimal Price { get; set; }
        public int ArtId { get; set; }
        public Art Art { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}