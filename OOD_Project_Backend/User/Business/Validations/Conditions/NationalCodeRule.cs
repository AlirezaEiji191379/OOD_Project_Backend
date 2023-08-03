using OOD_Project_Backend.Core.Validation.Contracts;

namespace OOD_Project_Backend.User.Business.Validations.Conditions;

public class NationalCodeRule : IRule
{
    private string _nationalCode;
    
    public NationalCodeRule(string nationalCode)
    {
        _nationalCode = nationalCode;
    }

    public bool Apply()
    {
        return !string.IsNullOrEmpty(_nationalCode) && _nationalCode.Length == 10;
    }
}