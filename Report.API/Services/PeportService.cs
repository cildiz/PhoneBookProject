﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Report.API.Constants;
using Report.API.Contexts;
using Report.API.Entities;
using Report.API.Enumerables;
using Report.API.Models;
using Report.API.Services.Base;
using Report.API.Services.Repositories;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;

namespace Contact.API.Services
{
    public class PeportService : BaseService, IPeportRepository
    {
        private readonly ReportContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ReportSettings _reportSettings;

        public PeportService(ReportContext context, IHttpClientFactory httpClientFactory, IOptions<ReportSettings> reportSettings) : base(context)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _reportSettings = reportSettings?.Value;
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

        public async Task GenerateStatisticsReport(Guid uuid)
        {
            var report = await _context.Reports.Where(x => x.UUID == uuid).FirstOrDefaultAsync();

            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_reportSettings.PhoneBookApiUrl}/Person/ContactInformations");
            var response = await client.SendAsync(request);

            var responseStream = await response.Content.ReadAsStringAsync();
            var contactInformations = JsonConvert.DeserializeObject<IEnumerable<ContactInformationModel>>(responseStream);

            var statisticsReport = contactInformations.Where(x => x.InformationType == 2).Select(x => x.InformationContent).Distinct().Select(x => new ReportDetail
            {
                ReportUUID = uuid,
                Location = x,
                PersonCount = contactInformations.Where(y => y.InformationType == 2 && y.InformationContent == x).Count(),
                PhoneNumberCount = contactInformations.Where(y => y.InformationType == 0 && contactInformations.Where(y => y.InformationType == 2 && y.InformationContent == x).Select(x => x.PersonUUID).Contains(y.PersonUUID)).Count()
            });
            report.ReportStatus = ReportStatus.Completed;

            await _context.ReportDetails.AddRangeAsync(statisticsReport);
            await _context.SaveChangesAsync();
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

        public async Task CreateRabbitMQPublisher(ReportRequestModel model, ReportSettings reportSettings)
        {
            var conn = reportSettings.RabbitMqCon;

            var createDocumentQueue = "create_document_queue";
            var documentCreateExchange = "document_create_exchange";

            ConnectionFactory connectionFactory = new()
            {
                Uri = new Uri(conn)
            };

            var connection = connectionFactory.CreateConnection();

            var channel = connection.CreateModel();
            channel.ExchangeDeclare(documentCreateExchange, "direct");

            channel.QueueDeclare(createDocumentQueue, false, false, false);
            channel.QueueBind(createDocumentQueue, documentCreateExchange, createDocumentQueue);

            channel.BasicPublish(documentCreateExchange, createDocumentQueue, null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model)));

        }
    }
}
