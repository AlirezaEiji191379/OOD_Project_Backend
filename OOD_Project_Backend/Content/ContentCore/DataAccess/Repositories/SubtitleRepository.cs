using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories;

public class SubtitleRepository : BaseRepository<SubtitleEntity>,ISubtitleRepository
{
    public SubtitleRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}