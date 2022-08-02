namespace VPMS.SharedKernel.Responses;

public sealed class ErrorResponse : BaseResponse
{
    public string? TraceId { get; init; }

    public ErrorResponse()
    {
        base.Success = false;
    }
}
