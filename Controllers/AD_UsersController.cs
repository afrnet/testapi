using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Test.API.Controllers
{
    [Route("apiAD/[controller]")]
    [ApiController]
    public class AD_UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AD_UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AD_User>>> GetUsers()
        {
            return await _context.AD_Users.ToListAsync();
        }

        [HttpGet("{login}")]
        public async Task<ActionResult<AD_User>> GetUser(string login)
        {
            var user = await _context.AD_Users.FindAsync(login);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<AD_User>> PostUser(AD_User user)
        {
            _context.AD_Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { login = user.Login_Name }, user);
        }

        [HttpPut("{login}")]
        public async Task<IActionResult> PutUser(string login, AD_User input)
        {
            var user = await _context.AD_Users.FindAsync(login);
            if (user == null) return NotFound();

            user.Authority_Level = input.Authority_Level;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{login}")]
        public async Task<IActionResult> DeleteUser(string login)
        {
            var user = await _context.AD_Users.FindAsync(login);
            if (user == null) return NotFound();

            _context.AD_Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
