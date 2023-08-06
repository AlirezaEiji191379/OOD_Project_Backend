using OOD_Project_Backend.Content.DataAccess.Entities;

namespace OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities;

public class ChannelEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? PicturePath { get; set; }
    public string? Description { get; set; }
    public string JoinLink { get; set; }
    
    public ICollection<ChannelMemberEntity> ChannelMemberEntities { get; set; }
    public ICollection<CategoryEntity> CategoryEntities { get; set; }
    public ICollection<ContentEntity> ContentEntities { get; set; }
}