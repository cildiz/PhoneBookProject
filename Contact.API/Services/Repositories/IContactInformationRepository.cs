using Contact.API.Entities;
using Contact.API.Models;

namespace Contact.API.Services.Repositories
{
    public interface IContactInformationRepository
    {
        Task<ReturnModel> AddContactInformation(ContactInformationModel model);
        Task<ReturnModel> DeleteContactInformation(Guid uuid);
        Task<ReturnModel> GetAllContactInformations();
    }
}
