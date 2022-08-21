using Contact.API.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact.API.Entities
{
    [Table("Persons")]
    public class Person : BaseEntity
    {
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Company { get; set; } = "";
        public virtual ICollection<ContactInformation>? ContactInformations { get; set; }
    }
}
