using paySimplexBusiness.Models;
using System;

namespace paySimplexBusiness.Util
{
    public static class BaseExtensions
    {
        public static BaseErrorModel ToBaseErrorModel(this Exception exception, string message)
        {
            return new BaseErrorModel
            {
                GenericNumber = exception.HResult,
                GenericMessage = exception.Message,
                Message = message
            };
        }

        public static BaseSuccessModel ToBaseSuccessModel(this object obj, string message)
        {
            return new BaseSuccessModel
            {
                Result = obj,
                Message = message
            };
        }
    }
}
