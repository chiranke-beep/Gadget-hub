using Microsoft.AspNetCore.Mvc;
using GadgetHub.Models;
using GadgetHub.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using GadgetHub.DTOs;

namespace GadgetHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly GadgetHubDbContext _context;
        public UserController(GadgetHubDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() => await _context.Users.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserCreateDto userDto)
        {
            var user = new User
            {
                ProfilePicture = userDto.ProfilePicture,
                FullName = userDto.FullName,
                UserName = userDto.UserName,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
                PhoneNumber = userDto.PhoneNumber,
                Street = userDto.Street,
                City = userDto.City,
                State = userDto.State,
                ZipCode = userDto.ZipCode,
                Country = userDto.Country
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            user.ProfilePicture = userDto.ProfilePicture;
            user.FullName = userDto.FullName;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.PasswordHash = userDto.PasswordHash;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Street = userDto.Street;
            user.City = userDto.City;
            user.State = userDto.State;
            user.ZipCode = userDto.ZipCode;
            user.Country = userDto.Country;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
