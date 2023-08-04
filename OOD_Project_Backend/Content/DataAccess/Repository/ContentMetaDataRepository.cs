using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.DataAccess.Repository;

public class ContentMetaDataRepository : BaseRepository<ContentMetaDataEntity>, IContentMetaDataRepository
{
    private readonly AppDbContext _appDbContext;

    public ContentMetaDataRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }


    public Task<ContentMetaDataEntity> FindByContentId(int contentId)
    {
        return _appDbContext.ContentMetaDatas.Where(x => x.ContentId == contentId).SingleAsync();
    }
}