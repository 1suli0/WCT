using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WCT.Infrastructure.DTOs.Input;
using WCT.Infrastructure.DTOs.Output;
using WCT.Infrastructure.Extensions;
using WCT.Infrastructure.Interfaces;
using WCT.Infrastructure.Utilities;
using WCT.Infrastructure.Utilities.Mapping;

namespace WCT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Role.Administrator)]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public ProductController(IRepositoryManager repositoryManager)
        {
            this._repositoryManager = repositoryManager;
        }

        [HttpPost]
        [Produces(typeof(OutProductDTO))]
        public async Task<IActionResult> CreateAsync([FromBody] InProductDTO productDTO)
        {
            if (await this._repositoryManager.ProductRepository
                .ExistAsync(productDTO.Name))
                return BadRequest("Product already exists in database.");

            var product = this._repositoryManager.ProductRepository
                .Create(ProductMapper.Map(productDTO));

            await this._repositoryManager.SaveAsync();

            return Ok(ProductMapper.Map(product));
        }

        [HttpGet]
        [Produces(typeof(OutProductDTO))]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync()
        {
            var products = await _repositoryManager.ProductRepository.GetAsync();

            return Ok(products.Select(i => ProductMapper.Map(i)));
        }

        [HttpPut("{id}")]
        [Produces(typeof(OutProductDTO))]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] InProductDTO productDTO)
        {
            var product = await _repositoryManager.ProductRepository.GetAsync(id);

            if (product == null)
                return BadRequest("Product doesn't exists in database.");

            product.Update(productDTO);

            await this._repositoryManager.SaveAsync();

            return Ok(ProductMapper.Map(product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var product = await _repositoryManager.ProductRepository.GetAsync(id);

            if (product == null)
                return BadRequest("Product doesn't exists in database.");

            _repositoryManager.ProductRepository.Delete(product);

            await this._repositoryManager.SaveAsync();

            return Ok();
        }
    }
}