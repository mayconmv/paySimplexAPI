using System;

namespace paySimplexBusiness.Models
{
    public class BaseErrorModel
    {
        public int GenericNumber { get; set; }
        public string GenericMessage { get; set; }
        public string Message { get; set; }
    }
}