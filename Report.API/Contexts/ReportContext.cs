using Report.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Report.API.Contexts
{
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Report.API.Entities.Report> Reports { get; set; }
        public virtual DbSet<ReportDetail> ReportDetails { get; set; }
    }
}
