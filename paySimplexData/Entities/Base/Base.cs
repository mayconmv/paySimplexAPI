using System;

namespace paySimplexData.Entities
{
    public class Base
    {
        public DateTime? CreateDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
