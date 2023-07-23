using OOD_Project_Backend.Core.Validation;

namespace OOD_Project_Backend.User.Business.Validation;

public class DefaultValidator : Validator
{
    public bool Validate(params Rule[] rules)
    {
        foreach (var rule in rules)
        {
            if (!rule.Apply())
            {
                return false;
            }
        }

        return true;
    }
}