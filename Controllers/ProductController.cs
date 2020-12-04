using Dwagen.DTO.Products;
using Dwagen.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// add new user in the system
        /// </summary>
        /// <param name="addProductDto"></param>
        /// <returns></returns>
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDto addProductDto)
        {
            try
            {
                _logger.LogInformation("Adding New Product");
                var result = await _productService.AddProductAsnc(addProductDto);
                if (result.IsCreatedSuccessfully)
                {
                    return Ok(result);
                }
                _logger.LogError(result.ErrorMessages.FirstOrDefault());
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="updateProductDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDto updateProductDto)
        {
            try
            {
                _logger.LogInformation("Adding New Product");
                var result = await _productService.UpdateProduct(updateProductDto);
                if (result)
                {
                    return Ok(result);
                }
                _logger.LogError("Error Happened while updating");
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get Products by userId
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(Guid productId, string include)
        {
            try
            {
                _logger.LogInformation("Adding New Product");
                var result = await _productService.GetProductById(productId, include);
                if (result != null)
                {
                    return Ok(result);
                }
                _logger.LogError("Error while getting product by Id");
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProductByUserId")]
        public async Task<IActionResult> GetProductByUserId(Guid userId, string include)
        {
            try
            {
                _logger.LogInformation("Adding New Product");
                var result = await _productService.GetProductByUserId(userId, include);
                if (result != null)
                {
                    return Ok(result);
                }
                _logger.LogError("Error while getting product by Id");
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
