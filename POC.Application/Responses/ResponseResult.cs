using FluentValidation.Results;
using POC.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace POC.Application.Responses;

public sealed class ResponseResult<T> : BaseResponse
{
    [Newtonsoft.Json.JsonIgnore] // to ignore the property when serializing
    [System.Text.Json.Serialization.JsonIgnore] // to ignore the property in swagger doc
    public HttpStatusCode HttpStatusCode { get; }

    public ResponseResult(T value, int totalRecordCount = 1) : base()
    {
        Success = value is not null;
        Data = value;
        _totalRecordCount = totalRecordCount;
    }

    public ResponseResult(IList<ValidationFailure> validationFailures) : base()
    {
        HttpStatusCode = HttpStatusCode.BadRequest;
        Success = false;
        Data = default;

        if (validationFailures.Count is not 0)
        {
            ValidationErrors.AddRange(validationFailures.Select(s => new KeyValuePair<string, IEnumerable<string>>(s.PropertyName, new[] { s.ErrorMessage })).ToList());
        }
    }

    public ResponseResult(ApplicationException ex) : base()
    {
        Success = false;
        Data = default;

        var errorMsg = new[] { ex.Message };

        switch (ex)
        {
            case BadRequestException e:
                HttpStatusCode = HttpStatusCode.BadRequest;
                ValidationErrors.Add(new KeyValuePair<string, IEnumerable<string>>(e.PropertyName, errorMsg));
                break;

            case ValidationException e:
                HttpStatusCode = HttpStatusCode.BadRequest;
                ValidationErrors.AddRange(e.ValdationErrors);
                break;

            case NotFoundException e:
                HttpStatusCode = HttpStatusCode.NotFound;
                ValidationErrors.Add(new KeyValuePair<string, IEnumerable<string>>(nameof(HttpStatusCode.NotFound), errorMsg));
                break;

        };
    }

    private int _totalRecordCount = 1;
    public int TotalRecordCount
    {
        get
        {
            if (Data is null)
            {
                _totalRecordCount = 0;
            }

            return _totalRecordCount;
        }
    }
    public T Data { get; private set; }

}
