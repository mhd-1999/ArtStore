using ArtStore.Models.BaseEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtStore.Models
{
    public class Art : BaseEntity
    {
        [Required(ErrorMessage = "Please enter name !"), StringLength(50, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 1)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please choose image !")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Please enter description !"), Column(TypeName = "ntext")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter price !")]
        public decimal Price { get; set; }

        public int SalePercent { get; set; }

        [Required(ErrorMessage = "Select category !")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}