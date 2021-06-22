using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> ValdationErrors { get; set; }

        public ValidationException(List<string> validationResult)
        {
            ValdationErrors = new List<string>();

            foreach (var validationError in validationResult)
            {
                ValdationErrors.Add(validationError);
            }
        }
    }
}
