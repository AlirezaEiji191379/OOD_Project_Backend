using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Finanace.DataAccess.Entities;

namespace OOD_Project_Backend.User.DataAccess.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePicPath { get; set; }
    public string? Biography { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public string? NationalCode { get; set; }
    
    public ICollection<ChannelMemberEntity> ChannelMemberEntities { get; set; }
    public ICollection<TransactionEntity> TransactionEntities { get; set; }
    public ICollection<RefundEntity> RefundEntities { get; set; }
}