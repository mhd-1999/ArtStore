using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtStore.Models.BaseEntities
{
    public class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            IsDeleted = false;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }
    }
}
