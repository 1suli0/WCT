using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
    [Authorize()]
    [ServiceFilter(typeof(UserValidation))]
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
            var userId = HttpContext.User.Claims
                    .First(c => c.Type == "userId")?.Value;

            if (await this._repositoryManager.ShoppingListRepository
                .ExistAsync(shoppingListDTO.Name, int.Parse(userId)))
                return BadRequest("Shopping list already exists in database.");

            var list = this._repositoryManager.ShoppingListRepository
                .Create(ShoppingListMapper.Map(shoppingListDTO, int.Parse(userId)));

            await this._repositoryManager.SaveAsync();

            //load dependencies
            list = await _repositoryManager.ShoppingListRepository
                .GetAsync(list.Name, int.Parse(userId), false);

            return Ok(ShoppingListMapper.Map(list));
        }

        [HttpPut("{name}")]
        [Produces(typeof(OutShoppingListDTO))]
        public async Task<IActionResult> UpdateAsync([FromRoute] string name,
            [FromBody] InShoppingListDTO shoppingListDTO)
        {
            var userId = this.HttpContext.User
               .Claims.First(c => c.Type == "userId").Value;

            if (!await this._repositoryManager.ShoppingListRepository
                .ExistAsync(name, int.Parse(userId)))
                return BadRequest("Shopping list doesn't exists in database, " +
                    "or you do not have access to it.");

            var list = await this._repositoryManager.ShoppingListRepository
                .GetAsync(name, int.Parse(userId));

            list.Update(shoppingListDTO);

            await this._repositoryManager.SaveAsync();

            //load dependencies
            list = await _repositoryManager.ShoppingListRepository
                .GetAsync(list.Name, int.Parse(userId), false);

            return Ok(ShoppingListMapper.Map(list));
        }

        [HttpGet("from/{from}/to/{to}")]
        [Produces(typeof(OutShoppingListDTO))]
        public async Task<IActionResult> GetAsync([FromRoute] long from,
          [FromRoute] long to)
        {
            var userId = this.HttpContext.User
                          .Claims.First(c => c.Type == "userId").Value;

            var startTime = DateTimeOffset.FromUnixTimeMilliseconds(from).DateTime;
            var endTime = DateTimeOffset.FromUnixTimeMilliseconds(to).DateTime;

            var list = await _repositoryManager.ShoppingListRepository
                .GetAsync(int.Parse(userId), startTime, endTime);

            return Ok(list.Select(i => ShoppingListMapper.Map(i)));
        }
    }
}