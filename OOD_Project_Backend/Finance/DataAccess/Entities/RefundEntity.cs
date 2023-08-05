using OOD_Project_Backend.Finanace.DataAccess.Entities.Enums;
using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Finanace.DataAccess.Entities;

public class RefundEntity
{
    public int Id { get; set; }
    public int TransactionId{ get; set; }
    public TransactionEntity Transaction { get; set; }
    public int UserId{ get; set; }
    public UserEntity User { get; set; }
    public double Amount{ get; set; }
    public DateTime CreatedAt{ get; set; }
    public RefundStatus Status { get; set; }
}