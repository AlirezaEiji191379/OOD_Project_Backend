using OOD_Project_Backend.Core.Validation.Contracts;

namespace OOD_Project_Backend.User.Business.Validations.Conditions;

public class BiographyRule : IRule
{
    private string _biography;

    public BiographyRule(string biography)
    {
        _biography = biography;
    }

    public bool Apply()
    {
        if (string.IsNullOrEmpty(_biography))
        {
            return true;
        }
        return _biography.Length < 70;
    }
}