using OOD_Project_Backend.Core.Validation.Contracts;

namespace OOD_Project_Backend.Core.Validation.Common;

public class PictureRule : IRule
{
    private IFormFile _picture;
    public PictureRule(IFormFile picture)
    {
        _picture = picture;
    }


    public bool Apply()
    {
        if (_picture == null)
        {
            return false;
        }

        if (_picture.Length == 0 || _picture.Length > 5 * 1024 * 1024)
        {
            return false;
        }
        var allowedExtensions = new[] { ".png", ".jpg"};
        var fileExtension = Path.GetExtension(_picture.FileName).ToLower();
        if (!allowedExtensions.Contains(fileExtension))
            return false;
        return true;
    }
}