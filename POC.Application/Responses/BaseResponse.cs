using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Application.Responses
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();

        public BaseResponse()
        {
            Success = true;
        }
    }
}
