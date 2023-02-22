using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;
using AutoMapper;
using API.DTOs;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        // in order to inject anything in a class you must create a constructor:
        // DataContext is a dependency; this allows us to have a session of our database available
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet] // this declares we're using an HTTP Get method to get the API/users
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() // returns all of the users from the database
        {
            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username) // returns a single user given their ID
        {
            return await _userRepository.GetMemberAsync(username);
        }
    }
}