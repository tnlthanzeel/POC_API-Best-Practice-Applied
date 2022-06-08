using System.Collections.Generic;

namespace POC.Application.Responses
{
    public abstract class BaseResponse
    {
        public bool Success { get; protected set; }
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; } = new();

        public BaseResponse()
        {
            Success = true;
        }
    }
}
