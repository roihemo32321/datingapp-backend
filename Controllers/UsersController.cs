using AutoMapper;
using dating_backend.DTOs;
using dating_backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace dating_backend.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Getting our user from the JWT Token we provided.
            var user = await _userRepository.GetUserByUsernameAsync(username); // Getting the user details from the database.

            if (user == null) return NotFound();

            _mapper.Map(memberUpdateDto, user); // Mapping the values into our user varibale using _automapper.

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user.");
        }
    }
}
