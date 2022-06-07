using FluentValidation.Results;
using POC.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.Application.Responses
{
    public sealed class ResponseResult<T> : BaseResponse
    {
        public ResponseResult(T value, int totalRecordCount = 1) : base()
        {
            Data = value;
            _totalRecordCount = totalRecordCount;
        }

        public ResponseResult(IReadOnlyCollection<ValidationFailure> validationFailures) : base()
        {
            Success = false;
            Data = default;

            if (validationFailures.Count > 0)
            {
                foreach (var error in validationFailures)
                {
                    base.ValidationErrors.Add(error.ErrorMessage);
                }
            }
        }

        private int _totalRecordCount = 1;
        public int TotalRecordCount
        {
            get
            {
                if (Data == null)
                {
                    _totalRecordCount = 0;
                }

                return _totalRecordCount;
            }

            //set
            //{
            //    _totalRecordCount = value;
            //}
        }
        public T Data { get; private set; }
    }
}
