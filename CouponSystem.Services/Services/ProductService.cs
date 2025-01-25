using CouponSystem.DataAccess.Data;
using CouponSystem.Models.Dtos.Helpers;
using CouponSystem.Models.Dtos.ProductDto;
using CouponSystem.Models.Entities;
using CouponSystem.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CouponSystem.Services.Services
{

    public interface IProductService
    {
        Task<ResponsDataByPage<List<ProductViewModel>>> GetAll(int pageIndex, int pageSize);
        Task<ProductViewModel> Get(Guid id);
        Task<ProductViewModel> Create(CreateProductDto dto);
        Task<ProductViewModel> Update(UpdateProductDto dto , Guid id);
        Task<bool> Delete(Guid id);


    }
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public async Task<ResponsDataByPage<List<ProductViewModel>>> GetAll(int page = 1, int pageSize = 10)
        {
            var totalProducts = await _context.Products.CountAsync();
            var products = await _context.Products.AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(product => new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    HashCode = product.HashCode,
                    SKU = product.SKU,
                    InStock = product.InStock,
                    Height = product.Height,
                    Width = product.Width,
                    Length = product.Length,
                    Weight = product.Weight,
                })
                .ToListAsync();

            var paginationMeta = new Pagination
            {
                current_page = page,
                per_page = pageSize,
                total = totalProducts,
                last_page = (int)Math.Ceiling((double)totalProducts / pageSize)
            };

            return new ResponsDataByPage<List<ProductViewModel>>
            {
                IsSuccess = true,
                Data = products,
                meta = paginationMeta
            };
        }
        public async Task<ProductViewModel> Get(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));

            try
            {
                var model = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
                if (model == null)
                    throw new KeyNotFoundException("Product not found");

                return MapToViewModel(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching product: {ex.Message}");
                throw new Exception("An error occurred while retrieving the product.");
            }
        }
        public async Task<ProductViewModel> Create(CreateProductDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

           
            try
            {
                var newProduct = new Product
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Quantity = dto.Quantity,
                    HashCode = dto.HashCode,
                    SKU = dto.SKU,
                    InStock = dto.InStock,
                    Height = dto.Height,
                    Width = dto.Width,
                    Length = dto.Length,
                    Weight = dto.Weight
                };

                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();
                return MapToViewModel(newProduct);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating product: {ex.Message}");
                throw new Exception("An error occurred while creating the product.");
            }
        }
        public async Task<ProductViewModel> Update(UpdateProductDto dto, Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));

            try
            {
                var model = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
                if (model == null)
                    throw new KeyNotFoundException("Product not found");

                model.Name = dto.Name;
                model.Price = dto.Price;
                model.Quantity = dto.Quantity;
                model.InStock = dto.InStock;
                model.SKU = dto.SKU;
                model.HashCode = dto.HashCode;
                model.Weight = dto.Weight;
                model.Width = dto.Width;
                model.Height = dto.Height;
                model.Length = dto.Length;

                _context.Products.Update(model);
                await _context.SaveChangesAsync();
                return MapToViewModel(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product: {ex.Message}");
                throw new Exception("An error occurred while updating the product.");
            }
        }
        public async Task<bool> Delete(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));

            try
            {
                var model = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
                if (model == null)
                    throw new KeyNotFoundException("Product not found");

                _context.Products.Remove(model);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product: {ex.Message}");
                throw new Exception("An error occurred while deleting the product.");
            }
        } 
        private ProductViewModel MapToViewModel(Product model)
        {
            return new ProductViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
                HashCode = model.HashCode,
                SKU = model.SKU,
                InStock = model.InStock,
                Height = model.Height,
                Width = model.Width,
                Length = model.Length,
                Weight = model.Weight
            };
        }

    }
}
