using Contact.API.Controllers;
using Contact.API.Enumerables;
using Contact.API.Models;
using Contact.API.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBookProject.Tests.Helpers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBookProject.Tests.Contact.API.Tests.Controllers
{
    public class ReportControllerTests
    {
        [Fact]
        public async Task CreatePersonWithValidParamsReturn201()
        {
            var mockPersonRepository = new Mock<IPersonRepository>();
            mockPersonRepository
                .Setup(x => x.AddPerson(It.IsAny<PersonModel>()))
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = true
                });

            var contactController = new ContactController(mockPersonRepository.Object, new Mock<IContactInformationRepository>().Object);

            var result = await contactController.CreatePerson(new PersonModel()
            {
                Name = "CAN",
                Surname = "ILDIZ",
                Company = "RISE TECHNOLOGY"
            });

            Assert.Equal(201, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task CreatePersonWithInvalidParamsReturn400()
        {
            var mockPersonRepository = new Mock<IPersonRepository>();
            mockPersonRepository
                .Setup(x => x.AddPerson(It.IsAny<PersonModel>()))
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = false
                });

            var contactController = new ContactController(mockPersonRepository.Object, new Mock<IContactInformationRepository>().Object);

            var result = await contactController.CreatePerson(null);

            var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.IsType<BadRequestResult>(badRequestResult);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task DeletePersonWithValidParamsReturn200()
        {
            var guid= Guid.NewGuid();
            var mockPersonRepository = new Mock<IPersonRepository>();
            mockPersonRepository
                .Setup(x => x.DeletePerson(guid))
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = true
                });

            var contactController = new ContactController(mockPersonRepository.Object, new Mock<IContactInformationRepository>().Object);

            var result = await contactController.DeletePerson(guid);

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task DeletePersonWithNoPersonValidParamsReturn400()
        {
            var mockPersonRepository = new Mock<IPersonRepository>();
            mockPersonRepository
                .Setup(x => x.DeletePerson(It.IsAny<Guid>()))
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = false
                });

            var contactController = new ContactController(mockPersonRepository.Object, new Mock<IContactInformationRepository>().Object);

            var result = await contactController.DeletePerson(Guid.NewGuid());

            Assert.Equal(404, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetAllPersonsReturn200()
        {
            var mockPersonRepository = new Mock<IPersonRepository>();
            mockPersonRepository
                .Setup(x => x.GetAllPersons())
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = true
                });

            var contactController = new ContactController(mockPersonRepository.Object, new Mock<IContactInformationRepository>().Object);

            var result = await contactController.GetAllPersons();

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetAllPersonsReturn404()
        {
            var mockPersonRepository = new Mock<IPersonRepository>();
            mockPersonRepository
                .Setup(x => x.GetAllPersons())
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = false
                });

            var contactController = new ContactController(mockPersonRepository.Object, new Mock<IContactInformationRepository>().Object);

            var result = await contactController.GetAllPersons();

            Assert.Equal(404, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task AddContactInformationWithValidParamsReturn201()
        {
            var mockContactInformationRepository = new Mock<IContactInformationRepository>();
            mockContactInformationRepository
                .Setup(x => x.AddContactInformation(It.IsAny<ContactInformationModel>()))
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = true
                });

            var contactController = new ContactController(new Mock<IPersonRepository>().Object,mockContactInformationRepository.Object);

            var result = await contactController.AddContactInformation(new ContactInformationModel()
            {
                InformationType = InformationType.EmailAddress,
                InformationContent = "canildiz@outlook.com",
                PersonUUID = Guid.NewGuid(),
            });

            Assert.Equal(201, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task AddContactInformationWithInvalidParamsReturn400()
        {
            var mockContactInformationRepository = new Mock<IContactInformationRepository>();
            mockContactInformationRepository
                .Setup(x => x.AddContactInformation(It.IsAny<ContactInformationModel>()))
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = false
                });

            var contactController = new ContactController(new Mock<IPersonRepository>().Object, mockContactInformationRepository.Object);

            var result = await contactController.AddContactInformation(null);

            var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.IsType<BadRequestResult>(badRequestResult);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task AddContactInformationReturn404()
        {
            var mockContactInformationRepository = new Mock<IContactInformationRepository>();
            mockContactInformationRepository
                .Setup(x => x.AddContactInformation(It.IsAny<ContactInformationModel>()))
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = false
                });

            var contactController = new ContactController(new Mock<IPersonRepository>().Object, mockContactInformationRepository.Object);

            var result = await contactController.AddContactInformation(new ContactInformationModel()
            {
                InformationType = InformationType.EmailAddress,
                InformationContent = "canildiz@outlook.com",
                PersonUUID = It.IsAny<Guid>(),
            });

            Assert.Equal(404, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task DeleteContactInformationWithValidParamsReturn200()
        {
            var guid = Guid.NewGuid();
            var mockContactInformationRepository = new Mock<IContactInformationRepository>();
            mockContactInformationRepository
                .Setup(x => x.DeleteContactInformation(guid))
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = true
                });

            var contactController = new ContactController(new Mock<IPersonRepository>().Object, mockContactInformationRepository.Object);

            var result = await contactController.DeleteContactInformation(guid);

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task DeleteContactInformationWithInValidParamsReturn400()
        {
            var mockContactInformationRepository = new Mock<IContactInformationRepository>();
            mockContactInformationRepository
                .Setup(x => x.DeleteContactInformation(It.IsAny<Guid>()))
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = false
                });

            var contactController = new ContactController(new Mock<IPersonRepository>().Object, mockContactInformationRepository.Object);

            var result = await contactController.DeleteContactInformation(Guid.NewGuid());

            Assert.Equal(404, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetAllContactInformationsReturn200()
        {
            var mockContactInformationRepository = new Mock<IContactInformationRepository>();
            mockContactInformationRepository
                .Setup(x => x.GetAllContactInformations())
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = true
                });

            var contactController = new ContactController(new Mock<IPersonRepository>().Object, mockContactInformationRepository.Object);

            var result = await contactController.GetAllContactInformations();

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetAllContactInformationsReturn404()
        {
            var mockContactInformationRepository = new Mock<IContactInformationRepository>();
            mockContactInformationRepository
                .Setup(x => x.GetAllContactInformations())
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = false
                });

            var contactController = new ContactController(new Mock<IPersonRepository>().Object, mockContactInformationRepository.Object);

            var result = await contactController.GetAllContactInformations();

            Assert.Equal(404, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetPersonDetailWithValidParamsReturn200()
        {
            var guid = Guid.NewGuid();
            var mockPersonRepository = new Mock<IPersonRepository>();
            mockPersonRepository
                .Setup(x => x.GetPersonDetail(guid))
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = true
                });

            var contactController = new ContactController(mockPersonRepository.Object, new Mock<IContactInformationRepository>().Object);

            var result = await contactController.GetPersonDetail(guid);

            Assert.Equal(200, TestHelper.GetStatusCodeFromActionResult(result));
        }

        [Fact]
        public async Task GetPersonDetailWithInValidParamsReturn404()
        {
            var mockPersonRepository = new Mock<IPersonRepository>();
            mockPersonRepository
                .Setup(x => x.GetPersonDetail(It.IsAny<Guid>()))
                .ReturnsAsync(() => new ReturnModel()
                {
                    IsSuccess = false
                });

            var contactController = new ContactController(mockPersonRepository.Object, new Mock<IContactInformationRepository>().Object);

            var result = await contactController.GetPersonDetail(Guid.NewGuid());

            Assert.Equal(404, TestHelper.GetStatusCodeFromActionResult(result));
        }
    }
}
