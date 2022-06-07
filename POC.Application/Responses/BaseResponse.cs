using System.Collections.Generic;

namespace POC.Application.Responses
{
    public abstract class BaseResponse
    {
        public bool Success { get; protected set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();

        public BaseResponse()
        {
            Success = true;
        }
    }
}
