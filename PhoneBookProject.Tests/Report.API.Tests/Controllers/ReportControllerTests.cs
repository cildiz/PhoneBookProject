using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using PhoneBookProject.Tests.Helpers;
using Report.API.Constants;
using Report.API.Contexts;
using Report.API.Controllers;
using Report.API.Enumerables;
using Report.API.Models;
using Report.API.Services;
using Report.API.Services.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBookProject.Tests.Peport.API.Tests.Controllers
{
    public class ReportControllerTests
    {
        //[Fact]
        //public async Task GenerateStatisticsReport_Should_Generate_Report()
        //{
        //    var reportId = Guid.Parse("a5ba5d9c-f6fa-4466-9ef6-508592438fc9");
        //    var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        //    mockHttpMessageHandler.Protected()
        //        .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
        //        .ReturnsAsync(new HttpResponseMessage
        //        {
        //            StatusCode = HttpStatusCode.OK,
        //            Content = new StringContent("[{'uuid': 'a5ba5d9c-f6fa-4466-9ef6-508592438fc9','informationType':2,'informationContent':'Mersin', 'personId': 'b890413c-2dca-499a-8578-5cfb14e0b1eb'}]"),
        //        });


        //    var mockFactory = new Mock<IHttpClientFactory>();
        //    var mockHost = new Mock<IWebHostEnvironment>();

        //    var client = new HttpClient(mockHttpMessageHandler.Object);
        //    mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        //    IOptions<ReportSettings> settings = Options.Create<ReportSettings>(new ReportSettings()
        //    {
        //        ApiUrl = "http://localhost"
        //    });

        //    var serviceCollection = new ServiceCollection().AddLogging();

        //    var context = new ReportContext(TestHelper.GetReportContextForInMemoryDb());

        //    var report = new Report.API.Entities.Report()
        //    {
        //        UUID = reportId,
        //        ReportStatus = ReportStatus.Preparing
        //    };

        //    await context.Reports.AddAsync(report);
        //    await context.SaveChangesAsync();

        //    var service = new ReportService(context, mockFactory.Object, settings, mockHost.Object);

        //    await service.GenerateStatisticsReport(reportId);
        //    var reportStatus = context.Reports.Where(x => x.UUID == reportId).FirstOrDefault().ReportStatus.GetHashCode();
        //    Assert.Equal(ReportStatus.Completed.GetHashCode(), reportStatus);
        //}

        [Fact]
        public async Task GetAllReportsReturn200()
        {
            var mockPeportRepository = new Mock<IReportRepository>();
            mockPeportRepository
                .Setup(x => x.GetAllReports())
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = true
                });

            var reportController = new ReportController(mockPeportRepository.Object, new Mock<IOptions<ReportSettings>>().Object);

            var result = await reportController.GetAllReports();

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetAllReportsReturn404()
        {
            var mockPeportRepository = new Mock<IReportRepository>();
            mockPeportRepository
                .Setup(x => x.GetAllReports())
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = false
                });

            var reportController = new ReportController(mockPeportRepository.Object, new Mock<IOptions<ReportSettings>>().Object);

            var result = await reportController.GetAllReports();

            Assert.Equal(404, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetReportDetailWithValidParamsReturn200()
        {
            var guid = Guid.NewGuid();
            var mockPeportRepository = new Mock<IReportRepository>();
            mockPeportRepository
                .Setup(x => x.GetReportDetail(guid))
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = true
                });

            var reportController = new ReportController(mockPeportRepository.Object, new Mock<IOptions<ReportSettings>>().Object);

            var result = await reportController.GetReportDetail(guid);

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetReportDetailWithInValidParamsReturn404()
        {
            var mockPeportRepository = new Mock<IReportRepository>();
            mockPeportRepository
                .Setup(x => x.GetReportDetail(It.IsAny<Guid>()))
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = false
                });

            var reportController = new ReportController(mockPeportRepository.Object, new Mock<IOptions<ReportSettings>>().Object);

            var result = await reportController.GetReportDetail(Guid.Empty);

            Assert.Equal(404, TestHelper.GetStatusCodeFromActionResult(result));
        }
    }
}
