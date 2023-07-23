namespace OOD_Project_Backend.Core.Validation;

public interface Validator
{
    bool Validate(params Rule[] rules);
}