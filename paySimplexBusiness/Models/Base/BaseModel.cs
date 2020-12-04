using System;

namespace paySimplexBusiness.Models
{
    public class BaseModel
    {
        public DateTime? CreateDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}