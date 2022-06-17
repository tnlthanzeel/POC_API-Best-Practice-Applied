namespace POC.Application.Responses;

public abstract class BaseResponse
{
    public bool Success { get; protected set; }
    
    private List<KeyValuePair<string, IEnumerable<string>>> _errors = new();
    public List<KeyValuePair<string, IEnumerable<string>>> Errors
    {
        get
        {
            return _errors;
        }
        set
        {
            _errors = value;
            Success = _errors.Count == 0;
        }
    }

    public BaseResponse()
    {
        Success = true;
    }
}
