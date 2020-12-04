using System.ComponentModel.DataAnnotations;

namespace paySimplexData.Entities
{
    public class State : Base
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
