﻿namespace POC.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public string PropertyName;

        public NotFoundException(string propertyName, string name, object key)
            : base($"{name} ({key}) is not found")
        {
            PropertyName = propertyName;
        }
    }
}
