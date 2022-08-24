using Contact.API.Contexts;

namespace Contact.API.Services.Base
{
    public class BaseService
    {
        public readonly ContactContext _context;

        public BaseService(ContactContext context)
        {
            _context = context;
        }
    }
}
