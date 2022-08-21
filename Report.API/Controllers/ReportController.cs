using Microsoft.AspNetCore.Mvc;
using Report.API.Models;
using Report.API.Services.Repositories;

namespace Report.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IPeportRepository _reportRepository;

        public ReportController(IPeportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpPut("Request")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReturnModel>> CreateReportRequest()
        {
            var result = await _reportRepository.CreateReportRequest();

            if (result.IsSuccess)
            {
                return Ok(result);
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
