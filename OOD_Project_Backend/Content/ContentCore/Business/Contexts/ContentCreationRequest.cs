using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.ContentCore.Business.Contexts;

public class ContentCreationRequest
{
    public ContentType Type{ get; set; }
    public IFormFile? File{ get; set; }
    public string? Value{ get; set; }
    public string Title{ get; set; }
    public string? Description { get; set; }
    public int ChannelId { get; set; }
    public bool IsPremium { get; set; }
    public double Price { get; set; }
    
}