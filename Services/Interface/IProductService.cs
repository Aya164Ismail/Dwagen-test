using Dwagen.DTO.Products;
using Dwagen.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Services.Interface
{
    public interface IProductService
    {
        /// <summary>
        /// Add new product in database
        /// </summary>
        /// <param name="addProductDto"></param>
        /// <returns></returns>
        Task<CreationState> AddProductAsnc(AddProductDto addProductDto);

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="updateProductDto"></param>
        /// <returns></returns>
        Task<bool> UpdateProduct(UpdateProductDto updateProductDto);

        /// <summary>
        /// Det product from database by Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<ProductDto> GetProductById(Guid productId, string include = null);

        /// <summary>
        /// Get user from database by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductDto>> GetProductByUserId(Guid userId, string include = null);
    }
}
