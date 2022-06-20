using FluentValidation.Results;
using Newtonsoft.Json;
using POC.Application.Exceptions;
using System.Net;

namespace POC.Application.Responses;

public sealed class ResponseResult : ResponseResult<EmptyObject>
{
    public ResponseResult() : base(default(EmptyObject))
    {
        Success = true;
    }

    public ResponseResult(IList<ValidationFailure> validationFailures) : base(default(EmptyObject))
    {
        HttpStatusCode = HttpStatusCode.BadRequest;

        if (validationFailures.Count is not 0)
        {
            Errors.AddRange(validationFailures.Select(s => new KeyValuePair<string, IEnumerable<string>>(s.PropertyName, new[] { s.ErrorMessage })).ToList());
        }
    }

    public ResponseResult(ApplicationException ex) : base(default(EmptyObject))
    {
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

    [JsonIgnore]
    public override EmptyObject? Data => base.Data;

    [JsonIgnore]
    public override int TotalRecordCount => base.TotalRecordCount;
}
