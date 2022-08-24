using Report.API.Enumerables;

namespace Report.API.Models
{
    public class ContactInformationModel
    {
        public Guid UUID { get; set; }
        public Guid PersonUUID { get; set; }
        public int InformationType { get; set; }
        public string InformationContent { get; set; } = "";
    }
}
