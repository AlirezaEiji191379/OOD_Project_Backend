using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finance.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Finanace.DataAccess.Repository;

public class TransactionRepository : BaseRepository<TransactionEntity>,ITransactionRepository
{
    public TransactionRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}