using ArtStore.Data.Infrastructure;
using ArtStore.Models;
using ArtStore.Services.BaseServices;
using System.Collections.Generic;
using System.Linq;

namespace ArtStore.Services
{
    public class ArtServices : BaseServices<Art>, IArtServices
    {
        public ArtServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Art> GetArtByCategory(int cateId, bool canLoadDelete = false)
        {
            return _unitOfWork.ArtRepository.GetQuery().Where(x => x.CategoryId == cateId
                                                            && x.IsDeleted == canLoadDelete).ToList();
        }
    }
}
