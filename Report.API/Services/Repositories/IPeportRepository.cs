using Report.API.Models;

namespace Report.API.Services.Repositories
{
    public interface IPeportRepository
    {
        Task<ReturnModel> CreateReportRequest();
        Task<ReturnModel> GetAllReports();
        Task<ReturnModel> GetReportDetail(Guid uuid);
    }
}
