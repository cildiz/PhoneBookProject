using Contact.API.Entities;
using Contact.API.Models;

namespace Contact.API.Services.Repositories
{
    public interface IPersonRepository
    {
        Task<ReturnModel> AddPerson(PersonModel model);

        Task<ReturnModel> DeletePerson(Guid uuid);

        Task<IQueryable<PersonModel>> GetAllPersons();

        Task<PersonDetailModel> GetPersonDetail(Guid uuid);

    }
}
