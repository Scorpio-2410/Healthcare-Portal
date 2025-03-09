using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Healthcare_Patient_Portal.Models;
using Healthcare_Patient_Portal.Features; // ✅ Ensure DTOs are referenced
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Healthcare_Patient_Portal.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HealthcarePortalContext _context;

        public UsersController(HealthcarePortalContext context)
        {
            _context = context;
        }

        // ✅ GET: api/users → Fetch all users (returns only relevant fields via DTO)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetUsers()
        {
            return await _context.Users
                .Select(u => new UserResponseDTO
                {
                    UserId = u.UserId,
                    RoleType = u.RoleType,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Dob = u.Dob
                })
                .ToListAsync();
        }

        // ✅ POST: api/users → Create a new user (Uses UserCreateDTO for input, returns UserResponseDTO)
        [HttpPost]
        public async Task<ActionResult<UserResponseDTO>> CreateUser([FromBody] UserCreateDTO userDto)
        {
            var user = new User
            {
                RoleType = userDto.RoleType,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Dob = userDto.Dob
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var response = new UserResponseDTO
            {
                UserId = user.UserId,
                RoleType = user.RoleType,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Dob = user.Dob
            };

            return CreatedAtAction(nameof(GetUsers), new { id = user.UserId }, response);
        }
    }
}


