using FluentValidation;
using POC.Application.Features.Users.Command.CreateUser;
using POC.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POC.Application.Validators
{
    public class Validator<TValidator> where TValidator : IValidator, new()
    {
        public static async Task ValidateAsync<TRequest, TResult>(TRequest request, SuccessResponse<TResult> result)
        {
            var validator = new TValidator() as AbstractValidator<TRequest>;

            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                result.Success = false;
                result.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    result.ValidationErrors.Add(error.ErrorMessage);
                }
            }
        }
    }
}
