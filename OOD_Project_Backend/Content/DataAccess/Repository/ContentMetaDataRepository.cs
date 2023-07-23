using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.DataAccess.Repository;

public class ContentMetaDataRepository : BaseRepository<ContentMetaDataEntity>
{
    public ContentMetaDataRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}