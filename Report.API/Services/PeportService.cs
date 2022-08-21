using Microsoft.EntityFrameworkCore;
using Report.API.Contexts;
using Report.API.Entities;
using Report.API.Enumerables;
using Report.API.Models;
using Report.API.Services.Base;
using Report.API.Services.Repositories;

namespace Contact.API.Services
{
    public class PeportService : BaseService, IPeportRepository
    {
        public PeportService(ReportContext context) : base(context)
        {
        }

        public async Task<ReturnModel> CreateReportRequest()
        {
            var report = new Report.API.Entities.Report
            {
                Date = DateTime.UtcNow,
                ReportStatus = ReportStatus.Preparing
            };

            await _context.Reports.AddAsync(report);

            await _context.SaveChangesAsync();

            if (report.UUID == Guid.Empty)
            {
                return new ReturnModel
                {
                    IsSuccess = false,
                    Message = "Rapor isteği oluşturulamadı.",
                    Model = null
                };
            }

            return new ReturnModel
            {
                IsSuccess = true,
                Message = "Rapor isteği başarılı bir şekilde eklendi.",
                Model = report
            };
        }

        public async Task<ReturnModel> GetAllReports()
        {
            var result = await _context.Reports.Select(r => new ReportModel()
            {
                UUID = r.UUID,
                Date=r.Date,
                ReportStatus = r.ReportStatus
            }).ToListAsync();


            if (result.Count == 0)
            {
                return new ReturnModel
                {
                    IsSuccess = false,
                    Message = "Listelenecek rapor kaydı bulunamadı.",
                    Model = null
                };
            }

            return new ReturnModel
            {
                IsSuccess = true,
                Message = "Rapor kayıtları başarılı bir şekilde listelendi.",
                Model = result
            };
        }

        public async Task<ReturnModel> GetReportDetail(Guid uuid)
        {
            var result = await _context.Reports.Where(r => r.UUID == uuid).Select(r =>
             new ReportDetailModel()
             {
                 Report = new ReportModel()
                 {
                     UUID = uuid,
                     ReportStatus = r.ReportStatus,
                     Date = r.Date
                 },
                 ReportDetails = r.ReportDetails.Select(rd => new StatisticModel()
                 {
                     UUID = rd.UUID,
                     Location = rd.Location,
                     PersonCount = rd.PersonCount,
                     PhoneNumberCount = rd.PhoneNumberCount
                 }).ToList()
             }).FirstOrDefaultAsync();

            if (result == null)
            {
                return new ReturnModel
                {
                    IsSuccess = false,
                    Message = "Rapor bulunamadı.",
                    Model = null
                };
            }

            return new ReturnModel
            {
                IsSuccess = true,
                Message = "Raporun detayı başarılı bir şekilde getirildi.",
                Model = result
            };
        }
    }
}
