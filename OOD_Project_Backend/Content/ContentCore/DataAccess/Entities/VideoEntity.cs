namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;

public class VideoEntity
{
    public int ContentId;
    public ContentEntity Content { get; set; }
    public int? SubtitleId { get; set; }
    public SubtitleEntity? Subtitle { get; set; }
    public int FileId { get; set; }
    public FileEntity File { get; set; }
}