using Contact.API.Enumerables;

namespace Contact.API.Models
{
    public class ContactInformationModel
    {
        public Guid UUID { get; set; }
        public InformationType InformationType { get; set; }
        public string InformationContent { get; set; }
        public Guid PersonId { get; set; }
    }
}
