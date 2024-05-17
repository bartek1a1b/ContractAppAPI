using AutoMapper;
using ContractAppAPI.Data;
using ContractAppAPI.Dto;
using ContractAppAPI.Interfaces;
using ContractAppAPI.Models;
using ContractAppAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractTypeTwoController : Controller
    {
        private readonly IContractTypeTwoRepository _contractTypeTwoRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ContractTypeTwoController(IContractTypeTwoRepository contractTypeTwoRepository, DataContext context, IMapper mapper)
        {
            _contractTypeTwoRepository = contractTypeTwoRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContractTypeTwo>))]
        public IActionResult GetContractTypeTwos()
        {
            var contractTypeTwos = _mapper.Map<List<ContractTypeTwoDto>>(_contractTypeTwoRepository.GetContractTypeTwos());


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(contractTypeTwos);
        }

        [HttpGet("{contractTypeTwoId}")]
        [ProducesResponseType(200, Type = typeof(ContractTypeTwo))]
        [ProducesResponseType(400)]
        public IActionResult GetContractTypeTwo(int contractTypeTwoId)
        {
            if (!_contractTypeTwoRepository.ContractTypeTwoExists(contractTypeTwoId))
            {
                return NotFound();
            }

            var contractTypeTwo = _mapper.Map<ContractTypeTwoDto>(_contractTypeTwoRepository.GetContractTypeTwo(contractTypeTwoId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(contractTypeTwo);
        }

        [HttpGet("/contractsTwo/{contractTypeTwoId}")]
        [ProducesResponseType(200, Type = typeof(ContractTypeTwo))]
        [ProducesResponseType(400)]
        public IActionResult GetContractByTypeTwo(int contractTypeTwoId)
        {
            var contracts = _mapper.Map<List<ContractDto>>(_contractTypeTwoRepository.GetContractByTypeTwo(contractTypeTwoId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(contracts);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateContractTypeTwo([FromBody] ContractTypeTwoDto contractTypeTwoCreate)
        {
            if (contractTypeTwoCreate == null)
            {
                return BadRequest(ModelState);
            }

            var contractTypeTwo = _contractTypeTwoRepository.GetContractTypeTwos()
                .Where(c => c.Name.Trim().ToUpper() == contractTypeTwoCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (contractTypeTwo != null)
            {
                ModelState.AddModelError("", "Typ umowy już istnieje");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contractTypeTwoMap = _mapper.Map<ContractTypeTwo>(contractTypeTwoCreate);

            if (!_contractTypeTwoRepository.CreateContractTypeTwo(contractTypeTwoMap))
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas zapisywania");
                return StatusCode(500, ModelState);
            }

            return Ok("Pomyślnie dodano typ");
        }

        [HttpPut("{contractTypeTwoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateContractTypeTwo(int contractTypeTwoId, [FromBody] ContractTypeTwoDto updatedContractTypeTwo)
        {
            if (updatedContractTypeTwo == null)
                return BadRequest(ModelState);

            if (contractTypeTwoId != updatedContractTypeTwo.Id)
                return BadRequest(ModelState);

            if (!_contractTypeTwoRepository.ContractTypeTwoExists(contractTypeTwoId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var contractTypeTwoMap = _mapper.Map<ContractTypeTwo>(updatedContractTypeTwo);

            if (!_contractTypeTwoRepository.UpdateContractTypeTwo(contractTypeTwoMap))
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas edycji");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize(Policy = "RequireWriterRole")]
        [HttpDelete("{contractTypeTwoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteContractTypeTwo(int contractTypeTwoId)
        {
            if (!_contractTypeTwoRepository.ContractTypeTwoExists(contractTypeTwoId))
            {
                return NotFound();
            }

            var contractTypeTwoDelete = _contractTypeTwoRepository.GetContractTypeTwo(contractTypeTwoId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_contractTypeTwoRepository.DeleteContractTypeTwo(contractTypeTwoDelete))
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas usuwania");
            }

            return NoContent();

        }
    }
}
