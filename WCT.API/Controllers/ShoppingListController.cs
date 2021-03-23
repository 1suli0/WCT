using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WCT.Infrastructure.DTOs.Input;
using WCT.Infrastructure.DTOs.Output;
using WCT.Infrastructure.Interfaces;
using WCT.Infrastructure.Utilities.Mapping;

namespace WCT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class ShoppingListController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public ShoppingListController(IRepositoryManager repositoryManager)
        {
            this._repositoryManager = repositoryManager;
        }

        [HttpPost]
        [Produces(typeof(OutShoppingListDTO))]
        public async Task<IActionResult> CreateAsync([FromBody] InShoppingListDTO shoppingListDTO)
        {
            var userId = HttpContext.User?.Claims?
                    .FirstOrDefault(c => c.Type == "userId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            if (await this._repositoryManager.ShoppingListRepository
                .ExistAsync(shoppingListDTO.Name))
                return BadRequest("Shopping list already exists in database.");

            var list = this._repositoryManager.ShoppingListRepository
                .Create(ShoppingListMapper.Map(shoppingListDTO, int.Parse(userId)));

            await this._repositoryManager.SaveAsync();

            //load dependencies
            list = await _repositoryManager.ShoppingListRepository
                .GetAsync(list.Name, false);

            return Ok(ShoppingListMapper.Map(list));
        }
    }
}