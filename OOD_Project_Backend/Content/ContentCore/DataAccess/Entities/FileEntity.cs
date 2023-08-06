using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;

public class FileEntity
{
    public int Id { get; set; }
    public string Format{ get; set; }
    public double Size{ get; set; }
    public string FilePath{ get; set; }
    public FileQuality Quality{ get; set; }
}