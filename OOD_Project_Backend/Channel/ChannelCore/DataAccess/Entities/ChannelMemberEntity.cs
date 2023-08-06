using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities.Enums;
using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities;

public class ChannelMemberEntity
{
    public int UserId { get; set; }
    public UserEntity User { get; set; }
    public int ChannelId { get; set; }
    public ChannelEntity Channel { get; set; }
    public Role Role { get; set; }
    public double IncomeShare { get; set; }
}