using OOD_Project_Backend.Finanace.DataAccess.Entities.Enums;
using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Finanace.DataAccess.Entities;

public class TransactionEntity
{
    public int Id{ get; set; }
    public int Amount{ get; set; }
    public int UserId{ get; set; }
    public UserEntity User { get; set; }
    public TransactionType Type{ get; set; }
    public TransactionStatus Status{ get; set; }
    public DateTime CreatedAt{ get; set; }
    public string Source{ get; set; }
    public string Destination{ get; set; }
    public string BankToken { get; set; }
}