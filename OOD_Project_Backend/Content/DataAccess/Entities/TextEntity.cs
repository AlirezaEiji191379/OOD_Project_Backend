namespace OOD_Project_Backend.Content.DataAccess.Entities;

public class TextEntity
{
    public int ContentId { get; set; }
    public ContentEntity Content { get; set; }
    public string Value { get; set; }
}