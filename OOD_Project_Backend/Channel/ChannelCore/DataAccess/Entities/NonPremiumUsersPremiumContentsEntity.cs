using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities;

public class NonPremiumUsersPremiumContentsEntity
{
    public int UserId { get; set; }
    public UserEntity User { get; set; }
    public int ContentId{ get; set; }
    public ContentEntity Content { get; set; }
}