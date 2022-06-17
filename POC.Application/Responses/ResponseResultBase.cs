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
        Data = null;
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

    public Object? Data { get; private set; } = null!;
}
