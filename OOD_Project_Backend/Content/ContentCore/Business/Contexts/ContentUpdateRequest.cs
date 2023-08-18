namespace OOD_Project_Backend.Content.ContentCore.Business.Contexts;

public class ContentUpdateRequest
{
    public IFormFile? File{ get; set; }
    public string? Value{ get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public string? Title{ get; set; }
    public int ContentId { get; set; }
    public int? CategoryId { get; set; }
}