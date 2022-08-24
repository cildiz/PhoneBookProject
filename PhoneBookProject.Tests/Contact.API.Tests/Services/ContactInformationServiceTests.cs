using Contact.API.Contexts;
using Contact.API.Entities;
using Contact.API.Enumerables;
using Contact.API.Models;
using Contact.API.Services;
using PhoneBookProject.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBookProject.Tests.Contact.API.Tests.Services
{
    public class ContactInformationServiceTests
    {
        [Fact]
        public async Task AddContactInformationWithValidParams()
        {
            var context = new ContactContext(TestHelper.GetContactContextForInMemoryDb());

            var guid = Guid.NewGuid();
            context.Persons.Add(new Person()
            {
                UUID = guid,
                Name = "CAN",
                Surname = "ILDIZ",
                Company = "RISE TECHNOLOGY"
            });

            await context.SaveChangesAsync();

            var service = new ContactInformationService(context);

            var result = await service.AddContactInformation( new ContactInformationModel()
            {
                PersonUUID = guid,
                InformationType = InformationType.Location,
                InformationContent = "TEKİRDAĞ"
            });

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task AddContactInformationWithInValidParams()
        {
            var context = new ContactContext(TestHelper.GetContactContextForInMemoryDb());

            var guid = Guid.NewGuid();
            context.Persons.Add(new Person()
            {
                UUID = guid,
                Name = "CAN",
                Surname = "ILDIZ",
                Company = "RISE TECHNOLOGY"
            });

            await context.SaveChangesAsync();

            var service = new ContactInformationService(context);

            var result = await service.AddContactInformation(new ContactInformationModel()
            {
                PersonUUID = Guid.Empty,
                InformationType = InformationType.Location,
                InformationContent = "TEKİRDAĞ"
            });

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task DeleteContactInformationWithValidParams()
        {
            var context = new ContactContext(TestHelper.GetContactContextForInMemoryDb());

            var guid = Guid.NewGuid();
            context.ContactInformations.Add(new ContactInformation()
            {
                UUID = guid,
                InformationType = InformationType.Location,
                InformationContent = "TEKİRDAĞ"
            });

            await context.SaveChangesAsync();

            var service = new ContactInformationService(context);

            var result = await service.DeleteContactInformation(guid);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DeleteContactInformationWithInValidParams()
        {
            var context = new ContactContext(TestHelper.GetContactContextForInMemoryDb());

            var guid = Guid.NewGuid();
            context.ContactInformations.Add(new ContactInformation()
            {
                UUID = guid,
                InformationType = InformationType.Location,
                InformationContent = "TEKİRDAĞ"
            });

            await context.SaveChangesAsync();

            var service = new ContactInformationService(context);

            var result = await service.DeleteContactInformation(Guid.Empty);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetAllContactInformationsWithFullList()
        {
            var context = new ContactContext(TestHelper.GetContactContextForInMemoryDb());

            var guid = Guid.NewGuid();
            context.ContactInformations.Add(new ContactInformation()
            {
                UUID = guid,
                InformationType = InformationType.Location,
                InformationContent = "Tekirdağ"
            });

            await context.SaveChangesAsync();

            var service = new ContactInformationService(context);

            var result = await service.GetAllContactInformations();

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetAllContactInformationsWithEmptyList()
        {
            var context = new ContactContext(TestHelper.GetContactContextForInMemoryDb());

            var service = new ContactInformationService(context);

            var result = await service.GetAllContactInformations();

            Assert.False(result.IsSuccess);
        }
    }
}
