using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;

        // in order to inject anything in a class you must create a constructor:
        // DataContext is a dependency; this allows us to have a session of our database available
        public UsersController(DataContext context)
        {
            _context = context;
            
        }

        [AllowAnonymous]
        [HttpGet] // this declares we're using an HTTP Get method to get the API/users
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() // returns all of the users from the database
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id) // returns a single user given their ID
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }
    }
}