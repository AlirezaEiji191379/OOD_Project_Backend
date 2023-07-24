using OOD_Project_Backend.Content.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.DataAccess.Dtos;

public class ContentDto
{
    public int ContentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public ContentType Type { get; set; }
    public string FileName { get; set; }
}