namespace OOD_Project_Backend.Channel.ChannelCore.Business.Context;

public class ChannelUpdateRequest
{
    public int ChannelId { get; set; }
    public string? ChannelName { get; set; }
    public string? Description { get; set; }
}