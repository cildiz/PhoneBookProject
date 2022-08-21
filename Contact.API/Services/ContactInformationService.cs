using Contact.API.Contexts;
using Contact.API.Entities;
using Contact.API.Models;
using Contact.API.Services.Base;
using Contact.API.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contact.API.Services
{
    public class ContactInformationService : BaseService, IContactInformationRepository
    {
        public ContactInformationService(ContactContext context) : base(context)
        {
        }

        public async Task<ReturnModel> AddContactInformation(ContactInformationModel model)
        {
            var person = await _context.Persons.Where(p => p.UUID == model.PersonUUID).FirstOrDefaultAsync();

            if (person == null)
            {
                return new ReturnModel()
                {
                    IsSuccess = false,
                    Message = "İletişim bilgisi eklenecek kişi bulunamadı.",
                    Model = null
                };
            }

            var contactInformation = new ContactInformation()
            {
                PersonUUID = model.PersonUUID,
                InformationType = model.InformationType,
                InformationContent = model.InformationContent,
            };

            await _context.AddAsync(contactInformation);
            await _context.SaveChangesAsync();

            if (contactInformation.UUID == Guid.Empty)
            {
                return new ReturnModel
                {
                    IsSuccess = false,
                    Message = "İletişim bilgisi eklenemedi.",
                    Model = null
                };
            }

            return new ReturnModel()
            {
                IsSuccess = true,
                Message = $"{person.Name} {person.Surname} için iletişim bilgisi eklendi.",
                Model = contactInformation
            };
        }

        public async Task<ReturnModel> DeleteContactInformation(Guid uuid)
        {
            var result = await _context.ContactInformations.Where(ci => ci.UUID == uuid).FirstOrDefaultAsync();

            if (result == null)
            {
                return new ReturnModel()
                {
                    IsSuccess = false,
                    Message = "Silinecek iletişim bilgisi bulunamadı.",
                    Model = null
                };
            }

            _context.ContactInformations.Remove(result);
            await _context.SaveChangesAsync();

            return new ReturnModel()
            {
                IsSuccess = true,
                Message = "İletişim bilgisi silindi.",
                Model = result
            };
        }

        public async Task<ReturnModel> GetAllContactInformations()
        {
            var result = await _context.ContactInformations.Select(ci => new ContactInformationModel
            {
                UUID = ci.UUID,
                InformationContent = ci.InformationContent,
                InformationType = ci.InformationType,
                PersonUUID = ci.PersonUUID
            }).ToListAsync();

            if (result.Count == 0)
            {
                return new ReturnModel
                {
                    IsSuccess = false,
                    Message = "Listelenecek iletişim bilgisi kaydı bulunamadı.",
                    Model = null
                };
            }

            return new ReturnModel
            {
                IsSuccess = true,
                Message = "İletişim bilgisi kayıtları başarılı bir şekilde listelendi.",
                Model = result
            };
        }

    }
}
