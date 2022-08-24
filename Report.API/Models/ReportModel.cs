using Report.API.Enumerables;

namespace Report.API.Models
{
    public class ReportModel
    {
        public Guid UUID { get; set; }
        public DateTime Date { get; set; }
        public ReportStatus ReportStatus { get; set; }
        public string FilePath { get; set; }
    }
}
