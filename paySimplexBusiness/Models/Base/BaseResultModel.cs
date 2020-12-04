using System;

namespace paySimplexBusiness.Models
{
    public class BaseResultModel
    {
        public string Message { get; set; }

        public object Result { get; set; }

        public bool Error { get; set; }
    }
}