using ABPCourse.Demo1.Bases;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Linq.Dynamic.Core;
using Volo.Abp.Domain.Repositories;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using ABPCourse.Demo1.Permissions;

namespace ABPCourse.Demo1.Products
{
    public class ProductsAppService : BaseApplicationService, IProductsAppService
    {
        #region field
        private readonly IRepository<Product, int> productsRepository;
        #endregion

        #region ctor
        public ProductsAppService(IRepository<Product, int> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        #endregion

        #region IProductsAppService
        [Authorize(Demo1Permissions.CreateEditProductPermission)]
        public async Task<ProductDto> CreateProductAsync(CreateUpdateProductDto input)
        {
            //VALIDATION
            var valid = new CreateUpdateProductValidator().Validate(input);
            if (!valid.IsValid)
            {
                var exception = GetValidationException(valid);
                throw exception;
            }
            var product = ObjectMapper.Map<CreateUpdateProductDto, Product>(input);
            var inserted = await productsRepository.InsertAsync(product, autoSave: true);
            return ObjectMapper.Map<Product, ProductDto>(inserted);
        }

        [Authorize(Demo1Permissions.DeleteProductPermission)]
        public async Task<bool> DeleteProductAsync(int id)
        {
            var existingProduct = await productsRepository.GetAsync(id);
            if (existingProduct != null)
            {
                return false;
            }
            await productsRepository.DeleteAsync(existingProduct);
            return true;
        }

        [Authorize(Demo1Permissions.ListProductPermission)]
        public async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
        {
           if(input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Product.Id);
            }

            var products = await productsRepository
                                .WithDetailsAsync(product => product.Category)
                                .Result
                                .AsQueryable()
                                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                                  product => product.NameAr.Contains(input.Filter) ||
                                             product.NameEn.Contains(input.Filter)
                                             )
                                .WhereIf(
                                    input.CategoryId.HasValue,
                                    product => product.CategoryId == input.CategoryId
                                 )
                                .Skip(input.SkipCount)
                                .Take(input.MaxResultCount)
                                .OrderBy(input.Sorting)
                                .ToListAsync();
            var totalCount = input.Filter == null 
                ? await productsRepository.CountAsync() 
                : await productsRepository.CountAsync(product => product.NameAr.Contains(input.Filter) ||
                                                      product.NameEn.Contains(input.Filter));
            return new PagedResultDto<ProductDto>(
                totalCount,
                ObjectMapper.Map<List<Product>, List<ProductDto>>(products));
                
        }

        [Authorize(Demo1Permissions.GetProductPermission)]
        public async Task<ProductDto> GetProductAsync(int id)
        {
            var product = await productsRepository
                               .WithDetailsAsync(product => product.Category)
                               .Result
                               .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                throw new ProductNotFoundException(id);
            }

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        [Authorize(Demo1Permissions.CreateEditProductPermission)]
        public async Task<ProductDto> UpdateProductAsync(CreateUpdateProductDto input)
        {
            //VALIDATION
            var valid = new CreateUpdateProductValidator().Validate(input);
            if (!valid.IsValid)
            {
                var exception = GetValidationException(valid);
                throw exception;
            }

            var existing = await productsRepository.GetAsync(input.Id);
            if (existing == null)
            {
                throw new ProductNotFoundException(input.Id);
            }
            var mapped = ObjectMapper.Map<CreateUpdateProductDto, Product>(input,existing);
            var updated = await productsRepository.UpdateAsync(mapped, autoSave: true);
            return ObjectMapper.Map<Product, ProductDto>(updated);
        }
        #endregion
    }
}
