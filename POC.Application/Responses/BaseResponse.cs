namespace POC.Application.Responses;

public abstract class BaseResponse
{
    public bool Success { get; protected set; }

    public virtual List<KeyValuePair<string, IEnumerable<string>>> Errors { get; init; } = new();

    public BaseResponse()
    {
        Success = false;
    }
}
