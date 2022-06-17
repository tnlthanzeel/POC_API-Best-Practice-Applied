namespace POC.Application.Responses;

public abstract class BaseResponse
{
    public bool Success { get; protected set; }
    public List<KeyValuePair<string, IEnumerable<string>>> Errors { get; set; } = new();

    public BaseResponse()
    {
        Success = true;
    }
}
