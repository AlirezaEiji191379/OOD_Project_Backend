using OOD_Project_Backend.Core.Validation.Contracts;

namespace OOD_Project_Backend.User.Business.Validations.Conditions;

public class NameRule : IRule
{
    private string _name;

    public NameRule(string name)
    {
        _name = name;
    }

    public bool Apply()
    {
        if (string.IsNullOrEmpty(_name))
        {
            return false;
        }

        return true;
    }
}