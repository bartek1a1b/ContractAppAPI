using AutoMapper;
using ContractAppAPI.Data;
using ContractAppAPI.Dto;
using ContractAppAPI.Interfaces;
using ContractAppAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnexToTheContractController : Controller
    {
        private readonly IAnnexToTheContractRepository _annexToTheContractRepository;
        private readonly IContractRepository _contractRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AnnexToTheContractController(IAnnexToTheContractRepository annexToTheContractRepository, IContractRepository contractRepository, 
            DataContext context, IMapper mapper)
        {
            _annexToTheContractRepository = annexToTheContractRepository;
            _contractRepository = contractRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AnnexToTheContract>))]
        public IActionResult GetAnnexToTheContracts()
        {
            var annexToTheContracts = _mapper.Map<List<AnnexToTheContractDto>>(_annexToTheContractRepository.GetAnnexToTheContracts());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(annexToTheContracts);
        }

        [HttpGet("{annexId}")]
        [ProducesResponseType(200, Type = typeof(AnnexToTheContract))]
        [ProducesResponseType(400)]
        public IActionResult GetAnnexToTheContract(int annexId)
        {
            if (!_annexToTheContractRepository.AnnexToTheContractExists(annexId))
            {
                return NotFound();
            }

            var annexToTheContract = _mapper.Map<AnnexToTheContractDto>(_annexToTheContractRepository.GetAnnexToTheContract(annexId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(annexToTheContract);
        }

        [Authorize(Policy = "RequireWriterRole")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAnnexToTheContract([FromQuery] int contractId, [FromBody] AnnexToTheContractAddDto annexToTheContractCreate)
        {
            if (annexToTheContractCreate == null)
            {
                return BadRequest(ModelState);
            }

            var annexToTheContracts = _annexToTheContractRepository.GetAnnexToTheContracts();

            var existingAnnex = annexToTheContracts.FirstOrDefault(a => a.Name.Trim().ToUpper() == annexToTheContractCreate.Name.TrimEnd().ToUpper());

            if (existingAnnex != null)
            {
                ModelState.AddModelError("", "Aneks już istnieje");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var annexMap = _mapper.Map<AnnexToTheContract>(annexToTheContractCreate);

            annexMap.Contract = _contractRepository.GetContract(contractId);

            if (!_annexToTheContractRepository.CreateAnnexToTheContract(contractId, annexMap))
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas dodawania aneksu");
                return StatusCode(500, ModelState);
            }

            return Ok("Pomyślnie dodano aneks");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("delete-annexToTheContract/{annexId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAnnexToTheContract(int annexId)
        {
            if (!_annexToTheContractRepository.AnnexToTheContractExists(annexId))
            {
                return NotFound();
            }

            var annexToDelete = _annexToTheContractRepository.GetAnnexToTheContract(annexId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_annexToTheContractRepository.DeleteAnnexToTheContract(annexToDelete))
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas usuwania aneksu");
            }

            return NoContent();
        }
    }
}