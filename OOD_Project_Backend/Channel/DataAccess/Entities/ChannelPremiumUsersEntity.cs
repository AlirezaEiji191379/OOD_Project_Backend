using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Channel.DataAccess.Entities;

public class ChannelPremiumUsersEntity
{
    public int UserId { get; set; }
    public UserEntity User { get; set; }
    public int ChannelId { get; set; }
    public ChannelEntity Channel { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool Active { get; set; }
}