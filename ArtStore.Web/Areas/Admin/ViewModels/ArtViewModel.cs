using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtStore.Web.Areas.Admin.ViewModels
{
    public class ArtViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Please enter description !"), Column(TypeName = "ntext")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter price !")]
        public decimal Price { get; set; }

        public int SalePercent { get; set; }

        public int CategoryId { get; set; }
    }
}