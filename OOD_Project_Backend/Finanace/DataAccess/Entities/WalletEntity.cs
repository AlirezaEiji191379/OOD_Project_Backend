using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Finanace.DataAccess.Entities;

public class WalletEntity
{
    public int UserId { get; set; }
    public UserEntity User { get; set; }
    public int Balance { get; set; }
}