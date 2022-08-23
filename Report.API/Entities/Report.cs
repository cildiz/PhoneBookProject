using Report.API.Entities.Base;
using Report.API.Enumerables;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report.API.Entities
{
    [Table("Reports")]
    public class Report : BaseEntity
    {
        public DateTime Date { get; set; }
        public ReportStatus ReportStatus { get; set; }
        public string FilePath { get; set; } = "";
        public virtual List<ReportDetail>? ReportDetails { get; set; }
    }
}
