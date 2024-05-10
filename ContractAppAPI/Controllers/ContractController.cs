using AutoMapper;
using ContractAppAPI.Data;
using ContractAppAPI.Dto;
using ContractAppAPI.Extensions;
using ContractAppAPI.Helper;
using ContractAppAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContractController(IContractRepository contractRepository, IContractTypeOneRepository contractTypeOneRepository, 
            IContractTypeTwoRepository contractTypeTwoRepository, DataContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _contractRepository = contractRepository;
            _contractTypeOneRepository = contractTypeOneRepository;
            _contractTypeTwoRepository = contractTypeTwoRepository;
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("search")]
        public async Task<ActionResult<PagedList<ContractDto>>> GetContractsDtosAsync([FromQuery] UserParams userParams, string searchPhrase)
        {
            var contracts = await _contractRepository.GetContractsDtosAsync(userParams, searchPhrase);

            Response.AddPaginationHeader(new PaginationHeader(contracts.CurrentPage, contracts.PageSize, contracts.TotalCount, contracts.TotalPages));

            var contractsDto = _mapper.Map<List<ContractDto>>(contracts);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(contractsDto);
        }

        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContractAppAPI.Models.Contract>))]
        public async Task<IActionResult> GetContractsAsync()
        {
            var contracts = await _contractRepository.GetContractsAsync();

            var contractsDto = _mapper.Map<List<ContractDto>>(contracts);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(contractsDto);
        }

        [HttpGet("dtos")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContractAppAPI.Models.Contract>))]
        public async Task<ActionResult<PagedList<ContractDto>>> GetContractsAsync([FromQuery] UserParams userParams)
        {
            var contracts = await _contractRepository.GetContractsAsync(userParams);

            Response.AddPaginationHeader(new PaginationHeader(contracts.CurrentPage, contracts.PageSize, contracts.TotalCount, contracts.TotalPages));

            var contractsDto = _mapper.Map<List<ContractDto>>(contracts);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(contractsDto);
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

        [HttpGet("{contractId}/annexes")]
        [ProducesResponseType(200, Type = typeof(ContractAppAPI.Models.Contract))]
        [ProducesResponseType(400)]
        public IActionResult GetAnnexByContract(int contractId)
        {
            if (!_contractRepository.ContractExists(contractId))
            {
                return NotFound();
            }

            var annexes = _mapper.Map<List<AnnexToTheContractDto>>(_contractRepository.GetAnnexByContract(contractId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(annexes);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateContract([FromQuery] int contractTypeOneId, [FromQuery] int contractTypeTwoId, [FromBody] ContractAddDto contractCreate)
        {
            if (contractCreate == null)
            {
                return BadRequest(ModelState);
            }

            var contracts = await _contractRepository.GetContractsAsync();

            var existingContract = contracts.FirstOrDefault(c => c.Name.Trim().ToUpper() == contractCreate.Name.TrimEnd().ToUpper());

            if (existingContract != null)
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

        [HttpPut("update/{conId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateContract(int conId, [FromQuery] int contractTypeOneId, [FromQuery] int contractTypeTwoId, [FromBody] ContractAddDto updatedContract)
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

        [HttpDelete("delete-contract/{conId}")]
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
