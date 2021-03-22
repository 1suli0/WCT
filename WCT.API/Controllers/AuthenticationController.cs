using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WCT.Infrastructure.DTOs.Input;
using WCT.Infrastructure.DTOs.Output;
using WCT.Infrastructure.Interfaces;
using WCT.Infrastructure.Utilities;

namespace WCT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IRepositoryManager repositoryManager,
            IConfiguration configuration)
        {
            this._repositoryManager = repositoryManager;
            this._configuration = configuration;
        }

        [Produces(typeof(OutTokenDTO))]
        [HttpPost("signIn")]
        public async Task<IActionResult> SignInAsync([FromBody] InSignInDTO signInDTO)
        {
            var user = await _repositoryManager.UserRepository
                .GetAsync(signInDTO.Email);

            if (user == null)
                return Unauthorized();

            var signInResult = await _repositoryManager.UserRepository
                .SignInAsync(user, signInDTO.Password);

            if (!signInResult.Succeeded)
                return Unauthorized();

            var roles = await _repositoryManager.UserRepository
                .GetRolesForUserAsync(user);

            var jwt = JWT.Generate(user, _configuration, roles);

            return Ok(jwt);
        }
    }
}