using System;
using System.Collections.Generic;

namespace POC.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<KeyValuePair<string, string>> ValdationErrors { get; set; } = new();

        public ValidationException(List<KeyValuePair<string, string>> validationResult)
        {

            foreach (var validationError in validationResult)
            {
                ValdationErrors.Add(new KeyValuePair<string, string>(validationError.Key, validationError.Value));
            }
        }
    }
}
