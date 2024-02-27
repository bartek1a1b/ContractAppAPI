using AutoMapper;
using ContractAppAPI.Data;
using ContractAppAPI.Dto;
using ContractAppAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContractAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : Controller
    {
        private readonly IContractRepository _contractRepository;
        private readonly IContractTypeOneRepository _contractTypeOneRepository;
        private readonly IContractTypeTwoRepository _contractTypeTwoRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ContractController(IContractRepository contractRepository, IContractTypeOneRepository contractTypeOneRepository, 
            IContractTypeTwoRepository contractTypeTwoRepository, DataContext context, IMapper mapper)
        {
            _contractRepository = contractRepository;
            _contractTypeOneRepository = contractTypeOneRepository;
            _contractTypeTwoRepository = contractTypeTwoRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContractAppAPI.Models.Contract>))]
        public IActionResult GetContracts()
        {
            var contracts = _mapper.Map<List<ContractDto>>(_contractRepository.GetContracts());


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(contracts);
        }

        [HttpGet("{conId}")]
        [ProducesResponseType(200, Type = typeof(ContractAppAPI.Models.Contract))]
        [ProducesResponseType(400)]
        public IActionResult GetContract(int conId)
        {
            if (!_contractRepository.ContractExists(conId))
            {
                return NotFound();
            }

            var contract = _mapper.Map<ContractDto>(_contractRepository.GetContract(conId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(contract);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateContract([FromQuery] int contractTypeOneId, [FromQuery] int contractTypeTwoId, [FromBody] ContractDto contractCreate)
        {
            if (contractCreate == null)
            {
                return BadRequest(ModelState);
            }

            var contracts = _contractRepository.GetContracts()
                .Where(c => c.Name.Trim().ToUpper() == contractCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (contracts != null)
            {
                ModelState.AddModelError("", "Umowa już istnieje");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contractMap = _mapper.Map<ContractAppAPI.Models.Contract>(contractCreate);

            contractMap.ContractTypeOne = _contractTypeOneRepository.GetContractTypeOne(contractTypeOneId);
            contractMap.ContractTypeTwo = _contractTypeTwoRepository.GetContractTypeTwo(contractTypeTwoId);

            if (!_contractRepository.CreateContract(contractTypeOneId, contractTypeTwoId, contractMap))
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas dodawania umowy");
                return StatusCode(500, ModelState);
            }

            return Ok("Pomyślnie dodano umowę");
        }

        [HttpPut("{conId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateContract(int conId, [FromQuery] int contractTypeOneId, [FromQuery] int contractTypeTwoId, [FromBody] ContractDto updatedContract)
        {
            if (updatedContract == null)
                return BadRequest(ModelState);

            if (conId != updatedContract.Id)
                return BadRequest(ModelState);

            if (!_contractRepository.ContractExists(conId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var contractMap = _mapper.Map<ContractAppAPI.Models.Contract>(updatedContract);

            contractMap.ContractTypeOne = _contractTypeOneRepository.GetContractTypeOne(contractTypeOneId);
            contractMap.ContractTypeTwo = _contractTypeTwoRepository.GetContractTypeTwo(contractTypeTwoId);

            if (!_contractRepository.UpdateContract(contractTypeOneId, contractTypeTwoId, contractMap))
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas edycji");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{conId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteContract(int conId)
        {
            if (!_contractRepository.ContractExists(conId))
            {
                return NotFound();
            }

            var contractToDelete = _contractRepository.GetContract(conId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_contractRepository.DeleteContract(contractToDelete))
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas usuwania umowy");
            }

            return NoContent();

        }
    }
}
