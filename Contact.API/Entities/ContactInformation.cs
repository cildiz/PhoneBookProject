using Contact.API.Entities.Base;
using Contact.API.Enumerables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact.API.Entities
{
    [Table("ContactInformations")]
    public class ContactInformation : BaseEntity
    {
        public Guid PersonUUID { get; set; }
        public InformationType InformationType { get; set; }
        public string InformationContent { get; set; }
    }
}
