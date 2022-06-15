namespace POC.Application.Responses
{
    public abstract class BaseResponse
    {
        public bool Success { get; protected set; }
        public List<KeyValuePair<string, IEnumerable<string>>> ValidationErrors { get; set; } = new();

        public BaseResponse()
        {
            Success = true;
        }
    }
}
