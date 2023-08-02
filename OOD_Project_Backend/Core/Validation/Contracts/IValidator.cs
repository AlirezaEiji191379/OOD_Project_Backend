namespace OOD_Project_Backend.Core.Validation.Contracts;

public interface IValidator
{
    bool Validate(params IRule[] rules);
}