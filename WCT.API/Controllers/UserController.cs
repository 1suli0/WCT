using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WCT.Infrastructure.DTOs.Input;
using WCT.Infrastructure.DTOs.Output;
using WCT.Infrastructure.Extensions;
using WCT.Infrastructure.Filters;
using WCT.Infrastructure.Interfaces;
using WCT.Infrastructure.Utilities.Mapping;

namespace WCT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ValidationFilter))]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public UserController(IRepositoryManager repositoryManager)
        {
            this._repositoryManager = repositoryManager;
        }

        [HttpPost()]
        [Produces(typeof(OutUserDTO))]
        public async Task<IActionResult> CreateAsync([FromBody] InUserDTO userDTO)
        {
            var user = UserMapper.Map(userDTO);

            var createResult = await _repositoryManager
                .UserRepository.CreateAsync(user, userDTO.Password);

            if (!createResult.Succeeded)
            {
                this.ModelState.AddErrors(createResult.Errors);
                return BadRequest();
            }

            return Ok(UserMapper.Map(user));
        }

        [Authorize()]
        [HttpPut("change/password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] InChangePasswordDTO changePasswordDTO)
        {
            var userId = this.HttpContext.User?
                .Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;

            if (userId == null)
                return Unauthorized();

            var user = await _repositoryManager.UserRepository
                .GetAsync(int.Parse(userId));

            if (user == null)
                return NotFound();

            var changePasswordResult = await _repositoryManager.UserRepository
                .ChangePasswordAsync(user, changePasswordDTO.Password,
                changePasswordDTO.NewPassword);

            if (!changePasswordResult.Succeeded)
                this.ModelState.AddErrors(changePasswordResult.Errors);

            return Ok();
        }
    }
}