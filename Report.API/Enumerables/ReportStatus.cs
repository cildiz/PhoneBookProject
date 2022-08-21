using System.ComponentModel;

namespace Report.API.Enumerables
{
    public enum ReportStatus
    {
        [Description("Hazırlanıyor")]
        Preparing,
        [Description("Tamamlandi")]
        Completed
    }
}
