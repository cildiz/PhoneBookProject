using Report.API.Contexts;

namespace Report.API.Services.Base
{
    public class BaseService
    {
        public readonly ReportContext _context;

        public BaseService(ReportContext context)
        {
            _context = context;
        }
    }
}
