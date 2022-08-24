using System.ComponentModel.DataAnnotations;

namespace Contact.API.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        [Required]
        public Guid UUID { get; set; }
    }
}
