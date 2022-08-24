using Report.API.Enumerables;

namespace Report.API.Models
{
    public class ReportDetailModel
    {
        public ReportModel? Report { get; set; }
        public List<StatisticModel>? ReportDetails { get; set; }
    }
}
