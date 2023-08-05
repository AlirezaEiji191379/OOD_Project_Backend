using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finanace.DataAccess.Entities.Enums;
using OOD_Project_Backend.Finance.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Finanace.DataAccess.Repository;

public class RefundRepository : BaseRepository<RefundEntity>,IRefundRepository
{
    private readonly AppDbContext _appDbContext;
    public RefundRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }

    public Task<List<RefundEntity>> FindAllWaitingIncludeTransaction()
    {
        return _appDbContext.Refunds.Where(x => x.Status == RefundStatus.WAITING).Include(x => x.Transaction).ToListAsync();
    }
}