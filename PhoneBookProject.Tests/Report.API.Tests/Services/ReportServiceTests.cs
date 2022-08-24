using Contact.API.Contexts;
using Contact.API.Entities;
using Contact.API.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using PhoneBookProject.Tests.Helpers;
using Report.API.Constants;
using Report.API.Contexts;
using Report.API.Enumerables;
using Report.API.Services;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Entities = Report.API.Entities;

namespace PhoneBookProject.Tests.Report.API.Tests.Services
{
    public class ReportServiceTests
    {
        [Fact]
        public async Task CreateReportRequestValidParams()
        {
            var context = new ReportContext(TestHelper.GetReportContextForInMemoryDb());

            var service = new ReportService(context, null, null, null);

            var result = await service.CreateReportRequest();

            Assert.True(result.IsSuccess == true);
        }

        [Fact]
        public async Task CreateReportRequestInValidParams()
        {
            var context = new ReportContext(TestHelper.GetReportContextForInMemoryDb());

            var service = new ReportService(context, null, null, null);

            var result = await service.CreateReportRequest();

            Assert.False(result.IsSuccess == false);
        }

        [Fact]
        public async Task GetAllReportsWithFullList()
        {
            var context = new ReportContext(TestHelper.GetReportContextForInMemoryDb());

            context.Reports.Add(new Entities.Report()
            {
                ReportStatus = ReportStatus.Preparing
            });

            await context.SaveChangesAsync();

            var service = new ReportService(context, null, null, null);

            var result = await service.GetAllReports();

            Assert.Equal(1, context.Reports.Local.Count);
        }

        [Fact]
        public async Task GetAllReportsWithEmptyList()
        {
            var context = new ReportContext(TestHelper.GetReportContextForInMemoryDb());

            var service = new ReportService(context, null, null, null);

            var result = await service.GetAllReports();

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetReportDetailWithValidParams()
        {
            var context = new ReportContext(TestHelper.GetReportContextForInMemoryDb());

            var guid = Guid.NewGuid();
            context.Reports.Add(new Entities.Report()
            {
                UUID = guid,
                ReportStatus = ReportStatus.Preparing,
                FilePath=@"C:\test.csv"
            });

            context.ReportDetails.Add(new Entities.ReportDetail()
            {
                Location = "Tekirdağ",
                PersonCount = 1,
                PhoneNumberCount = 1,
                ReportUUID = guid
            });

            await context.SaveChangesAsync();

            var service = new ReportService(context, null, null, null);

            var result = await service.GetReportDetail(guid);

            Assert.NotNull(result.Model);
        }

        [Fact]
        public async Task GetReportDetailWithInValidParams()
        {
            var context = new ReportContext(TestHelper.GetReportContextForInMemoryDb());

            var guid = Guid.NewGuid();
            context.Reports.Add(new Entities.Report()
            {
                UUID = guid,
                ReportStatus = ReportStatus.Preparing,
                FilePath = @"C:\test.csv"
            });

            context.ReportDetails.Add(new Entities.ReportDetail()
            {
                Location = "Tekirdağ",
                PersonCount = 1,
                PhoneNumberCount = 1,
                ReportUUID = guid
            });

            await context.SaveChangesAsync();

            var service = new ReportService(context, null, null, null);

            var result = await service.GetReportDetail(Guid.Empty);

            Assert.Null(result.Model);
        }
        //[Fact]
        //public async Task GenerateStatisticsReportNotFoundThrowError()
        //{
        //    var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        //    mockHttpMessageHandler.Protected()
        //        .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
        //        .ReturnsAsync(new HttpResponseMessage
        //        {
        //            StatusCode = HttpStatusCode.OK,
        //            Content = new StringContent("{\"isSuccess\": true, \"message\": \"İletişim bilgisi kayıtları başarılı bir şekilde listelendi.\", \"model\": [ {\"uuid\": \"9d50d4c0-1e4e-4b13-8ab9-12824ce30b98\",\"personUUID\": \"22888271-e867-4e34-b62e-2db97239a557\",\"informationType\": 2,\"informationContent\": \"TEKIRDAG\"}]}"),
        //        });

        //    var mockFactory = new Mock<IHttpClientFactory>();

        //    var client = new HttpClient(mockHttpMessageHandler.Object);
        //    mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        //    IOptions<ReportSettings> settings = Options.Create<ReportSettings>(new ReportSettings()
        //    {
        //        ApiUrl = "http://localhost"
        //    });

        //    var serviceCollection = new ServiceCollection().AddLogging();
        //    var logger = serviceCollection.BuildServiceProvider().GetService<ILogger<ReportService>>();

        //    var context = new ReportContext(TestHelper.GetReportContextForInMemoryDb());

        //    var service = new ReportService(context, mockFactory.Object, settings,null);

        //    Func<Task> func = () => service.GenerateStatisticsReport(Guid.Parse("9d50d4c0-1e4e-4b13-8ab9-12824ce30b98"));             //assert
        //    Exception exception = await Assert.ThrowsAsync<Exception>(func);
        //}
    }
}
