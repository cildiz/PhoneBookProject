using Report.API.Enumerables;

namespace Report.API.Models
{
    public class StatisticModel
    {
        public Guid UUID { get; set; }
        public string Location { get; set; } = "";
        public string FilePath { get; set; } = "";
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }
    }
}
