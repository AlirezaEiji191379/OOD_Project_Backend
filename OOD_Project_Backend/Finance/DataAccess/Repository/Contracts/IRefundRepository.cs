using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.Finanace.DataAccess.Entities;

namespace OOD_Project_Backend.Finance.DataAccess.Repository.Contracts;

public interface IRefundRepository : IBaseRepository<RefundEntity>
{
    Task<List<RefundEntity>> FindAllWaitingIncludeTransaction();
}