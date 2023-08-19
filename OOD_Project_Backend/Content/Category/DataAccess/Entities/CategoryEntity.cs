using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities;

namespace OOD_Project_Backend.Content.Category.DataAccess.Entities;

public class CategoryEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ChannelId { get; set; }
    public ChannelEntity Channel { get; set; }
}