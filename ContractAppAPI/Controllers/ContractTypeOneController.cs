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
    public class ContractTypeOneController : Controller
    {
        private readonly IContractTypeOneRepository _contractTypeOneRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ContractTypeOneController(IContractTypeOneRepository contractTypeOneRepository, DataContext context, IMapper mapper)
        {
            _contractTypeOneRepository = contractTypeOneRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContractTypeOne>))]
        public IActionResult GetContractTypeOnes()
        {
            var contractTypeOnes = _mapper.Map<List<ContractTypeOneDto>>(_contractTypeOneRepository.GetContractTypeOnes());


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(contractTypeOnes);
        }

        [HttpGet("{contractTypeOneId}")]
        [ProducesResponseType(200, Type = typeof(ContractTypeOne))]
        [ProducesResponseType(400)]
        public IActionResult GetContractTypeOne(int contractTypeOneId)
        {
            if (!_contractTypeOneRepository.ContractTypeOneExists(contractTypeOneId))
            {
                return NotFound();
            }

            var contractTypeOne = _mapper.Map<ContractTypeOneDto>(_contractTypeOneRepository.GetContractTypeOne(contractTypeOneId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(contractTypeOne);
        }

        [HttpGet("/contractsOne/{contractTypeOneId}")]
        [ProducesResponseType(200, Type = typeof(ContractTypeOne))]
        [ProducesResponseType(400)]
        public IActionResult GetContractByTypeOne(int contractTypeOneId)
        {
            var contracts = _mapper.Map<List<ContractDto>>(_contractTypeOneRepository.GetContractByTypeOne(contractTypeOneId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(contracts);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateContractTypeOne([FromBody] ContractTypeOneDto contractTypeOneCreate)
        {
            if (contractTypeOneCreate == null)
            {
                return BadRequest(ModelState);
            }

            var contractTypeOne = _contractTypeOneRepository.GetContractTypeOnes()
                .Where(c => c.Name.Trim().ToUpper() == contractTypeOneCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (contractTypeOne != null)
            {
                ModelState.AddModelError("", "Typ umowy już istnieje");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contractTypeOneMap = _mapper.Map<ContractTypeOne>(contractTypeOneCreate);

            if (!_contractTypeOneRepository.CreateContractTypeOne(contractTypeOneMap))
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas zapisywania");
                return StatusCode(500, ModelState);
            }

            return Ok("Pomyślnie dodano typ");
        }

        [HttpPut("{contractTypeOneId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateContractTypeOne(int contractTypeOneId, [FromBody] ContractTypeOneDto updatedContractTypeOne)
        {
            if (updatedContractTypeOne == null)
                return BadRequest(ModelState);

            if (contractTypeOneId != updatedContractTypeOne.Id)
                return BadRequest(ModelState);

            if (!_contractTypeOneRepository.ContractTypeOneExists(contractTypeOneId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var contractTypeOneMap = _mapper.Map<ContractTypeOne>(updatedContractTypeOne);

            if (!_contractTypeOneRepository.UpdateContractTypeOne(contractTypeOneMap))
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas edycji");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize(Policy = "RequireWriterRole")]
        [HttpDelete("delete-contractTypeOne/{contractTypeOneId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteContractTypeOne(int contractTypeOneId)
        {
            if (!_contractTypeOneRepository.ContractTypeOneExists(contractTypeOneId))
            {
                return NotFound();
            }

            var contractTypeOneDelete = _contractTypeOneRepository.GetContractTypeOne(contractTypeOneId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_contractTypeOneRepository.DeleteContractTypeOne(contractTypeOneDelete))
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas usuwania");
            }

            return NoContent();

        }
    }
}
