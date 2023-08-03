using OOD_Project_Backend.Core.Validation.Contracts;

namespace OOD_Project_Backend.User.Business.Validations.Conditions;

public class ProfilePictureRule : IRule
{
    private IFormFile _picture;
    public ProfilePictureRule(IFormFile picture)
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