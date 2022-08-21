using Contact.API.Models;
using Contact.API.Services;
using Contact.API.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IContactInformationRepository _contactInformationRepository;

        public ContactController(IPersonRepository personRepository, IContactInformationRepository contactInformationRepository)
        {
            _personRepository = personRepository;
            _contactInformationRepository = contactInformationRepository;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReturnModel>> CreatePerson([FromBody] PersonModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var result = await _personRepository.AddPerson(model);

            if (result.IsSuccess)
            {
                return Created("", result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("{uuid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnModel>> DeletePerson([FromRoute] Guid uuid)
        {
            var result = await _personRepository.DeletePerson(uuid);

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PersonModel>>> GetAllPersons()
        {
            var result = await _personRepository.GetAllPersons();

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPut("{uuid}/ContactInformations")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnModel>> AddContactInformation([FromBody] ContactInformationModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var result = await _contactInformationRepository.AddContactInformation(model);

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Created("", result);
        }

        [HttpDelete("ContactInformations/{uuid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnModel>> DeleteContactInformation([FromRoute] Guid uuid)
        {
            var result = await _contactInformationRepository.DeleteContactInformation(uuid);

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("ContactInformations")]
        public async Task<ActionResult<IEnumerable<ContactInformationModel>>> GetAllContactInformations()
        {
            var result = await _contactInformationRepository.GetAllContactInformations();

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("{uuid}/Detail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnModel>> GetPersonDetail(Guid uuid)
        {
            var result = await _personRepository.GetPersonDetail(uuid);

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
