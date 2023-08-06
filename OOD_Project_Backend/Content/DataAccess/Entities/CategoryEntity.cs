using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Entities;

namespace OOD_Project_Backend.Content.DataAccess.Entities;

public class CategoryEntity
{
    public int Id { get; set; }
    private string Title { get; set; }
    public int ChannelId { get; set; }
    public ChannelEntity Channel { get; set; }
}