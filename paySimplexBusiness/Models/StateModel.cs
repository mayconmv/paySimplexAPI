using paySimplexResources;
using System.ComponentModel.DataAnnotations;

namespace paySimplexBusiness.Models
{
    public class StateModel : BaseModel
    {
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(StateResources), ErrorMessageResourceName = "NameRequired")]
        public string Name { get; set; }
    }
}
