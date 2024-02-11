using ContractAppAPI.Data;
using ContractAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractAppAPI.Controllers
{
    public class RoleController : BaseApiController
    {
        private readonly DataContext _context;

        public RoleController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();

            return roles;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            return await _context.Roles.FindAsync(id);
        }
    } 
}
