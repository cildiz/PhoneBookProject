using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact.API.Entities
{
    [Table("Persons")]
    public class Person
    {
        [Key]
        [Required]
        public Guid UUID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public virtual ICollection<ContactInformation> ContactInformations { get; set; }
    }
}
