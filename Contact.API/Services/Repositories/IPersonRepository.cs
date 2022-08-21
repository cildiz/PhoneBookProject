using Contact.API.Entities;
using Contact.API.Models;

namespace Contact.API.Services.Repositories
{
    public interface IPersonRepository
    {
        Task<ReturnModel> AddPerson(PersonModel model);

        Task<ReturnModel> DeletePerson(Guid uuid);

        Task<ReturnModel> GetAllPersons();

        Task<ReturnModel> GetPersonDetail(Guid uuid);

    }
}
