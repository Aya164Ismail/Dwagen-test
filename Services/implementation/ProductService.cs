using AutoMapper;
using Dwagen.DTO.Products;
using Dwagen.Model.Entities;
using Dwagen.Repository;
using Dwagen.Repository.UnitOfWork;
using Dwagen.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dwagen.Services.implementation
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = webHostEnvironment;
        }
        public async Task<CreationState> AddProductAsnc(AddProductDto addProductDto)
        {
            var creationState = new CreationState { IsCreatedSuccessfully = false, CreatedObjectId = null };

            var newProduct = _mapper.Map<AddProductDto, Products>(addProductDto);
            await _unitOfWork.ProductRepository.CreateAsync(newProduct);
            creationState.IsCreatedSuccessfully = await _unitOfWork.SaveAsync() > 0;
            creationState.CreatedObjectId = newProduct.Id;
            string extention = Path.GetExtension(addProductDto.File.FileName);
            string path = _hostEnvironment.WebRootPath + "/Uploads/" + newProduct.Id + extention;
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await addProductDto.File.CopyToAsync(stream);
            }

            return creationState;
        }

        public async Task<bool> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var product = await _unitOfWork.ProductRepository.FindByIdAsync(updateProductDto.Id);
            if(product != null)
            {
                var newProduct = _mapper.Map<UpdateProductDto, Products>(updateProductDto);
                _unitOfWork.ProductRepository.Update(newProduct);

                return await _unitOfWork.SaveAsync() > 0;

            }
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<ProductDto> GetProductById(Guid productId, string include = null)
        {
            var product = await _unitOfWork.ProductRepository.FindElementAsync(x => x.Id == productId, include);
            if (product != null)
            {
                var productDto = _mapper.Map<Products, ProductDto>(product);
                return productDto;
            }
            return null;
        }

        public async Task<IEnumerable<ProductDto>> GetProductByUserId(Guid userId, string include = null)
        {
            var product = await _unitOfWork.ProductRepository.GetElementsAsync(x => x.UserId == userId, include);
            if (product != null)
            {
                var productDto = _mapper.Map<IEnumerable<Products>, IEnumerable<ProductDto>>(product);
                return productDto.OrderBy(x => x.CreatedDate);
            }
            return null;
        }
    }
}
