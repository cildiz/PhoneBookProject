using Report.API.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report.API.Entities
{
    [Table("ReportDetails")]
    public class ReportDetail : BaseEntity
    {
        public Guid ReportUUID { get; set; }
        public string Location { get; set; } = "";
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }
    }
}
