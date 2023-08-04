using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finance.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Finanace.DataAccess.Repository;

public class WalletRepository : BaseRepository<WalletEntity>,IWalletRepository
{
    private readonly AppDbContext _appDbContext;
    public WalletRepository(AppDbContext dbContext, AppDbContext appDbContext) : base(dbContext)
    {
        _appDbContext = appDbContext;
    }

    public Task<WalletEntity> FindByUserId(int userId)
    {
        return _appDbContext.Wallets.Where(x => x.UserId == userId).SingleAsync();
    }
}