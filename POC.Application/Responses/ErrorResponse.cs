﻿using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Application.Responses
{
    public sealed class ErrorResponse : BaseResponse
    {
        public ErrorResponse()
        {
            base.Success = false;
        }
    }
}
