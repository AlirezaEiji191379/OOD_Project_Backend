namespace OOD_Project_Backend.Content.ContentCore.Business.Contracts;

public class ContentDto
{
    
    public int ContentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Type { get; set; }
    public string FileName { get; set; }
    public double Price { get; set; }
    public bool IsPremium { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
}