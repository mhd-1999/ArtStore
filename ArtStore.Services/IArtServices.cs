using ArtStore.Models;
using ArtStore.Services.BaseServices;
using System.Collections.Generic;

namespace ArtStore.Services
{
    public interface IArtServices:IBaseService<Art>
    {
        IEnumerable<Art> GetArtByCategory(int cateId, bool canLoadDelete = false);
    }
}
