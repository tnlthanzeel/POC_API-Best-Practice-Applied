using FluentValidation.Results;
using POC.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace POC.Application.Responses;

public sealed class ResponseResult<T> : BaseResponse
{
    [Newtonsoft.Json.JsonIgnore] // ingore the property when serializing
    [System.Text.Json.Serialization.JsonIgnore] // ingore the property in swagger doc
    public HttpStatusCode HttpStatusCode { get; }

    public ResponseResult(T value, int totalRecordCount = 1) : base()
    {
        Data = value;
        _totalRecordCount = totalRecordCount;
    }

    public ResponseResult(IList<ValidationFailure> validationFailures) : base()
    {
        HttpStatusCode = HttpStatusCode.BadRequest;
        Success = false;
        Data = default;

        if (validationFailures.Count > 0)
        {
            ValidationErrors.AddRange(validationFailures.Select(s => s.ErrorMessage).ToList());
        }
    }

    public ResponseResult(ApplicationException ex) : base()
    {
        Success = false;
        Data = default;

        switch (ex)
        {
            case BadRequestException:
                HttpStatusCode = HttpStatusCode.BadRequest;
                ValidationErrors.Add(ex.Message);
                break;

            case ValidationException e:
                HttpStatusCode = HttpStatusCode.BadRequest;
                ValidationErrors.AddRange(e.ValdationErrors);
                break;

            case NotFoundException:
                HttpStatusCode = HttpStatusCode.NotFound;
                ValidationErrors.Add(ex.Message);
                break;

        };
    }

    private int _totalRecordCount = 1;
    public int TotalRecordCount
    {
        get
        {
            if (Data == null)
            {
                _totalRecordCount = 0;
            }

            return _totalRecordCount;
        }
    }
    public T Data { get; private set; }

}
