using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace paySimplexData.Entities
{
    public class Task : Base
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public long? EstimatedTime { get; set; }

        public DateTime? EndDate { get; set; }

        public string FileName { get; set; }

        public byte[] FileContent { get; set; }

        [ForeignKey("State")]
        public long StateId { get; set; }
        public virtual State State { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
