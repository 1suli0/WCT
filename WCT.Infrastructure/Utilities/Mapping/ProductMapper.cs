using WCT.Core;
using WCT.Infrastructure.DTOs.Input;
using WCT.Infrastructure.DTOs.Output;

namespace WCT.Infrastructure.Utilities.Mapping
{
    public static class ProductMapper
    {
        public static Product Map(InProductDTO productDTO)
        {
            return new Product
            {
                Name = productDTO.Name,
                Price = productDTO.Price
            };
        }

        public static OutProductDTO Map(Product product)
        {
            return new OutProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}