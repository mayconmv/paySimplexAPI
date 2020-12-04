using paySimplexResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace paySimplexBusiness.Models
{
    public class TaskModel : BaseModel
    {
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(TaskResources), ErrorMessageResourceName = "TitleRequired")]
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public long? EstimatedTime { get; set; }
        public DateTime? EndDate { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }

        [Required(ErrorMessageResourceType = typeof(TaskResources), ErrorMessageResourceName = "StateRequired")]
        public long StateId { get; set; }
        public StateModel State { get; set; }

        [Required(ErrorMessageResourceType = typeof(TaskResources), ErrorMessageResourceName = "UserRequired")]
        public long UserId { get; set; }
        public UserModel User { get; set; }
    }

    public class TaskEstimatedTimeModel
    {
        public long Days { get; set; }
        public long Hours { get; set; }
        public long Minutes { get; set; }
    }
}
