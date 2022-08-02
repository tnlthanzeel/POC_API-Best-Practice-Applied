using FluentValidation;
using FluentValidation.Results;

namespace VPMS.SharedKernel.Validators;

public class Validator<TValidator> where TValidator : IValidator, new()
{
    public static async Task<ValidationResult> ValidateAsync<TRequest>(TRequest request, CancellationToken token)
    {
        var validator = new TValidator() as AbstractValidator<TRequest>;

        var validationResult = await validator!.ValidateAsync(request, cancellation: token);

        return validationResult;
    }
}
