using System;

namespace ArtStore.Models.BaseEntities
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        bool IsDeleted { get; set; }
    }
}
