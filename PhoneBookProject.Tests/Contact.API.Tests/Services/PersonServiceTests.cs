using Contact.API.Contexts;
using Contact.API.Entities;
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
    public class PersonServiceTests
    {
        [Fact]
        public async Task AddPersonWithValidParams()
        {
            var context = new ContactContext(TestHelper.GetContactContextForInMemoryDb());

            context.Persons.Add(new Person()
            {
                Name = "CAN",
                Surname = "ILDIZ",
                Company = "RISE TECHNOLOGY"
            });

            await context.SaveChangesAsync();

            Assert.Equal(1, context.Persons.Local.Count);
        }

        [Fact]
        public async Task AddPersonWithInvalidParams()
        {
            var context = new ContactContext(TestHelper.GetContactContextForInMemoryDb());

            var guid = Guid.NewGuid();

            context.Persons.Add(new Person()
            {
                UUID = Guid.Empty,
                Name = "CAN",
                Surname = "ILDIZ",
                Company = "RISE TECHNOLOGY"
            });

            await context.SaveChangesAsync();

            var result = await context.SaveChangesAsync();

            Assert.NotEqual(1, result);
        }

        [Fact]
        public async Task DeletePersonWithValidParams()
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

            var service = new PersonService(context);

            var result = await service.DeletePerson(guid);

            Assert.Equal(0, context.Persons.Local.Count);
        }

        [Fact]
        public async Task DeletePersonWithInvalidParams()
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

            var service = new PersonService(context);

            var result = await service.DeletePerson(Guid.Empty);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetAllPersonsWithFullList()
        {
            var context = new ContactContext(TestHelper.GetContactContextForInMemoryDb());

            context.Persons.Add(new Person()
            {
                Name = "CAN",
                Surname = "ILDIZ",
                Company = "RISE TECHNOLOGY"
            });

            await context.SaveChangesAsync();

            var service = new PersonService(context);

            var result = await service.GetAllPersons();

            Assert.Equal(true, result.IsSuccess);
        }

        [Fact]
        public async Task GetAllPersonsWithEmptyList()
        {
            var context = new ContactContext(TestHelper.GetContactContextForInMemoryDb());

            var service = new PersonService(context);

            var result = await service.GetAllPersons();

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetPersonDetailWithValidParams()
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

            var service = new PersonService(context);

            var result = await service.GetPersonDetail(guid);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetPersonDetailWithInvalidParams()
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

            var service = new PersonService(context);

            var result = await service.GetPersonDetail(Guid.Empty);

            Assert.False(result.IsSuccess);
        }
    }
}
