using Microsoft.AspNetCore.Mvc;
using ChatService.Models;
using ChatService.Repositories;

namespace ChatService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignInController : ControllerBase
    {
        private readonly UsersRepository _usersRepository;

        public SignInController(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        // POST: api/signin
        [HttpPost]
        public IActionResult SignIn([FromBody] SignInRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var user = _usersRepository.ValidateUser(request.UserName, request.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // For a real app you'd return a token; for the assignment we just return the user info
            return Ok(new
            {
                user.Id,
                user.UserName
            });
        }
    }
}
