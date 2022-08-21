using Contact.API.Contexts;
using Contact.API.Entities;
using Contact.API.Models;
using Contact.API.Services.Base;
using Contact.API.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contact.API.Services
{
    public class PersonService : BaseService, IPersonRepository
    {
        public PersonService(ContactContext context) : base(context)
        {
        }

        public async Task<ReturnModel> AddPerson(PersonModel model)
        {
            var person = new Person()
            {
                Name = model.Name,
                Surname = model.Surname,
                Company = model.Company
            };

            await _context.Persons.AddAsync(person);

            await _context.SaveChangesAsync();

            if (person.UUID == Guid.Empty)
            {
                return new ReturnModel
                {
                    IsSuccess = false,
                    Message = "Kişi eklenemedi.",
                    Model = null
                };
            }

            return new ReturnModel
            {
                IsSuccess = true,
                Message = "Kişi başarılı bir şekilde eklendi.",
                Model = person
            };
        }

        public async Task<ReturnModel> DeletePerson(Guid uuid)
        {
            var result = await _context.Persons.Where(p => p.UUID == uuid).FirstOrDefaultAsync();

            if (result == null)
            {
                return new ReturnModel
                {
                    IsSuccess = false,
                    Message = "Silinecek kişi bulunamadı.",
                    Model = null
                };
            }

            _context.Persons.Remove(result);

            await _context.SaveChangesAsync();

            return new ReturnModel
            {
                IsSuccess = true,
                Message = "Kişi silindi.",
                Model = result
            };
        }

        public Task<IQueryable<PersonModel>> GetAllPersons()
        {
            throw new NotImplementedException();
        }

        public Task<PersonDetailModel> GetPersonDetail(Guid uuid)
        {
            throw new NotImplementedException();
        }
    }
}
