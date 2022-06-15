using FluentValidation;
using FluentValidation.Results;

namespace POC.Application.Validators;

public class Validator<TValidator> where TValidator : IValidator, new()
{
    public static async Task<ValidationResult> ValidateAsync<TRequest>(TRequest request)
    {
        var validator = new TValidator() as AbstractValidator<TRequest>;

        var validationResult = await validator!.ValidateAsync(request);

        return validationResult;
    }
}
