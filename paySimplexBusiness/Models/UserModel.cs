using paySimplexResources;
using System.ComponentModel.DataAnnotations;

namespace paySimplexBusiness.Models
{
    public class UserModel : BaseModel
    {
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(UserResources), ErrorMessageResourceName = "NameRequired")]
        public string Name { get; set; }
    }
}
