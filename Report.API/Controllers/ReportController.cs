using Contact.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Report.API.Constants;
using Report.API.Models;
using Report.API.Services.Repositories;

namespace Report.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportSettings _reportSettings;
        private readonly IPeportRepository _reportRepository;

        public ReportController(IPeportRepository reportRepository, IOptions<ReportSettings> reportSettings)
        {
            _reportRepository = reportRepository;
            _reportSettings = reportSettings?.Value;
        }

        [HttpPut("Request")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReturnModel>> CreateReportRequest()
        {
            var result = await _reportRepository.CreateReportRequest();

            if (result.IsSuccess)
            {
                var model = new ReportRequestModel()
                {
                    ReportId = ((Report.API.Entities.Report)result.Model).UUID
                };

                await _reportRepository.CreateRabbitMQPublisher(model, _reportSettings);
                return Accepted("", result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReportModel>> GetAllReports()
        {
            var result = await _reportRepository.GetAllReports();

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("{uuid}/Detail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnModel>> GetReportDetail(Guid uuid)
        {
            var result = await _reportRepository.GetReportDetail(uuid);

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
