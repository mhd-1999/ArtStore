using ArtStore.Data.Infrastructure;
using ArtStore.Models;
using ArtStore.Services.BaseServices;

namespace ArtStore.Services
{
    public class CategorytServices : BaseServices<Category>, ICategoryServices
    {
        public CategorytServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
