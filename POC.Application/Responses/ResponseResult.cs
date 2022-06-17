using FluentValidation.Results;
using Newtonsoft.Json;
using POC.Application.Exceptions;
using System.Net;

namespace POC.Application.Responses;

public class ResponseResult : BaseResponse
{
    [JsonIgnore]
    public HttpStatusCode HttpStatusCode { get; protected set; }

    public ResponseResult() : base()
    {
        _totalRecordCount = 0;
    }

    public ResponseResult(IList<ValidationFailure> validationFailures) : base()
    {
        HttpStatusCode = HttpStatusCode.BadRequest;
        Success = false;

        if (validationFailures.Count is not 0)
        {
            ValidationErrors.AddRange(validationFailures.Select(s => new KeyValuePair<string, IEnumerable<string>>(s.PropertyName, new[] { s.ErrorMessage })).ToList());
        }
    }

    public ResponseResult(ApplicationException ex) : base()
    {
        _totalRecordCount = 0;
        Success = false;

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

            case NotFoundException:
                HttpStatusCode = HttpStatusCode.NotFound;
                ValidationErrors.Add(new KeyValuePair<string, IEnumerable<string>>(nameof(HttpStatusCode.NotFound), errorMsg));
                break;

        };
    }

    private int _totalRecordCount = 1;
    public virtual int TotalRecordCount
    {
        get
        {
            _totalRecordCount = 0;
            return _totalRecordCount;
        }
    }

    public object? Data = null;
}

public sealed class ResponseResult<T> : ResponseResult
{
    public ResponseResult(T? value, int totalRecordCount = 1) : base()
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
        _totalRecordCount = 0;
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

            case NotFoundException:
                HttpStatusCode = HttpStatusCode.NotFound;
                ValidationErrors.Add(new KeyValuePair<string, IEnumerable<string>>(nameof(HttpStatusCode.NotFound), errorMsg));
                break;

        };
    }

    private int _totalRecordCount = 1;
    public override int TotalRecordCount
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
    public T? Data { get; private set; }

}
