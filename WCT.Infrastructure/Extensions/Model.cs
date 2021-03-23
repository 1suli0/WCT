using WCT.Core;
using WCT.Infrastructure.DTOs.Input;

namespace WCT.Infrastructure.Extensions
{
    public static class Model
    {
        public static void Update(this Product product, InProductDTO productDTO)
        {
            product.Name = productDTO.Name;
            product.Price = productDTO.Price;
        }
    }
}