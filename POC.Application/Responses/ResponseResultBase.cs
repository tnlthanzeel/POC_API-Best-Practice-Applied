using FluentValidation.Results;
using Newtonsoft.Json;
using POC.Application.Exceptions;
using System.Net;

namespace POC.Application.Responses;

public class ResponseResult : BaseResponse
{

    [JsonIgnore]
    public HttpStatusCode HttpStatusCode { get; protected set; }

    public ResponseResult() : base() { }

    public ResponseResult(IList<ValidationFailure> validationFailures) : base()
    {
        HttpStatusCode = HttpStatusCode.BadRequest;
        Success = false;

        if (validationFailures.Count is not 0)
        {
            Errors.AddRange(validationFailures.Select(s => new KeyValuePair<string, IEnumerable<string>>(s.PropertyName, new[] { s.ErrorMessage })).ToList());
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
                Errors.Add(new KeyValuePair<string, IEnumerable<string>>(e.PropertyName, errorMsg));
                break;

            case ValidationException e:
                HttpStatusCode = HttpStatusCode.BadRequest;
                Errors.AddRange(e.ValdationErrors);
                break;

            case NotFoundException:
                HttpStatusCode = HttpStatusCode.NotFound;
                Errors.Add(new KeyValuePair<string, IEnumerable<string>>(nameof(HttpStatusCode.NotFound), errorMsg));
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

    public EmptyObject? Data => null;
}
