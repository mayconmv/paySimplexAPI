using paySimplexResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace paySimplexBusiness.Models
{
    public class ApplicationUserModel : BaseModel
    {
        public ApplicationUserModel()
        {
            Id = DateTime.Now.Ticks;
            Name = "Usuário da API";
        }

        public long Id { get; set; }
        public string Name { get; set; }
    }
}
