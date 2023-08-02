using OOD_Project_Backend.Core.Validation.Contracts;

namespace OOD_Project_Backend.Core.Validation;

public class Validator : IValidator
{
    public bool Validate(params IRule[] rules)
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