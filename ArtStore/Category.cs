using ArtStore.Models.BaseEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArtStore.Models
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Please enter name !"), StringLength(30, ErrorMessage = "Please not input bigger than {0} and less than {1}", MinimumLength = 5)]
        public string Name { get; set; }
        public virtual ICollection<Art> Arts { get; set; }
    }
}