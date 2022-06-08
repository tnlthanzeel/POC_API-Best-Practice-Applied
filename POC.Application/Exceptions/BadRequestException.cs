﻿using System;

namespace POC.Application.Exceptions;

public class BadRequestException : ApplicationException
{
    public string PropertyName = null;

    public BadRequestException(string propertyName, string message) : base(message)
    {
        PropertyName = propertyName;
    }
}
