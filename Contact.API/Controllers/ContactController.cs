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

        public ContactController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpPost]
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
                return BadRequest(result.Message);
            }
        }
    }
}
