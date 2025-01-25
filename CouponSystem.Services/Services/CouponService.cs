using CouponSystem.DataAccess.Data;
using CouponSystem.Models.Dtos.CouponDto;
using CouponSystem.Models.Dtos.Helpers;
using CouponSystem.Models.Entities;
using CouponSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace CouponSystem.Services.Services
{
    public interface ICouponService
    {
        Task<ResponsDataByPage<object>> GetAll(int pageIndex, int pageSize);
        Task<ResponseData<object>> Get(Guid id);
        Task<ResponseData<object>> GetCouponProducts(Guid id);
        Task<ResponseData<object>> GetCouponUsers(Guid id);
        Task<ResponseData<object>> Create(CreateCouponDto dto);
        Task<ResponseData<object>> Update(Guid id , UpdateCouponDto dto);
        Task<ResponseData<object>> Delete(Guid id);

    }
    public class CouponService : ICouponService
    {
        private readonly ApplicationDbContext _context;
        public CouponService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponsDataByPage<object>> GetAll(int pageIndex, int pageSize)
        {
            if (pageIndex <= 0 || pageSize <= 0)
                return new ResponsDataByPage<object>
                {
                    IsSuccess = false,
                    Message = "Page index and size must be greater than zero."
                };

            try
            {
                var totalCoupons = await _context.Coupons.CountAsync();
                var coupons = await _context.Coupons.AsNoTracking()
                    .Include(c => c.CouponUsers)
                    .Include(c => c.CouponProducts)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(coupon => new CouponViewModel
                    {
                        Id = coupon.Id,
                        Code = coupon.Code,
                        Name = coupon.Name,
                        MinimumOrderPrice = coupon.MinimumOrderPrice,
                        UseCount = coupon.UseCount,
                        Value = coupon.Value,
                        Type = coupon.Type,
                        StartDate = coupon.StartDate,
                        EndDate = coupon.EndDate,
                        FreeShipping = coupon.FreeShipping,
                        CODNotIncluded = coupon.CODNotIncluded,
                        CouponUsers = coupon.CouponUsers.Select(cu => new CouponUserViewModel
                        {
                            UserId = cu.UserId,
                            EmailAddress = cu.User.Email,
                            FullName = cu.User.FullName,
                            Usage = cu.Usage
                        }).ToList(),
                        CouponProducts = coupon.CouponProducts.Select(cp => new CouponProductViewModel
                        {
                            Id = cp.ProductId,
                            Name = cp.Product.Name,
                        }).ToList()
                    })
                    .ToListAsync();

                var paginationMeta = new Pagination
                {
                    current_page = pageIndex,
                    per_page = pageSize,
                    total = totalCoupons,
                    last_page = (int)Math.Ceiling((double)totalCoupons / pageSize)
                };

                return new ResponsDataByPage<object>
                {
                    IsSuccess = true,
                    Message = "Coupon Fetched Successfully",
                    Data = coupons,
                    meta = paginationMeta
                };
            }
            catch (Exception ex)
            {
                return new ResponsDataByPage<object>
                {
                    IsSuccess = false,
                    Message = "An error occurred while retrieving coupons."
                };
            }
        }
        public async Task<ResponseData<object>> Get(Guid id)
        {
            if (id == Guid.Empty)
                return new ResponseData<object>
                {
                    IsSuccess = false,
                    Message = "Coupon ID cannot be null or empty",
                };
            
            try
            {
                var coupon = await _context.Coupons
                    .Include(c => c.CouponUsers).ThenInclude(x=>x.User)
                    .Include(c => c.CouponProducts).ThenInclude(x => x.Product)
                    .Where(x => x.Id == id).FirstOrDefaultAsync();

                if (coupon == null)
                    throw new KeyNotFoundException("Coupon not found");

                return new ResponseData<object>
                {
                    IsSuccess = true,
                    Message = "Coupon Fetched Successfully",
                    Data = coupon,
                };
            }
            catch (Exception ex)
            { 
                return new ResponseData<object>
                {
                    IsSuccess = false,
                    Message = "An error occurred while retrieving the coupon.",
                 };
            }
        }

        public async Task<ResponseData<object>> GetCouponUsers(Guid id)
        {
            if (id == Guid.Empty)
                return new ResponseData<object>
                {
                    IsSuccess = false,
                    Message = "Coupon ID cannot be null or empty",
                };

            try
            {
                var coupon = await _context.Coupons
                   .Include(c => c.CouponUsers).ThenInclude(x => x.User)
                   .Include(c => c.CouponProducts).ThenInclude(x => x.Product)
                   .Where(x => x.Id == id).FirstOrDefaultAsync();

                if (coupon == null)
                    throw new KeyNotFoundException("Coupon not found");

                return new ResponseData<object>
                {
                    IsSuccess = true,
                    Message = "Coupon Fetched Successfully",
                    Data = coupon.CouponUsers,
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<object>
                {
                    IsSuccess = false,
                    Message = "An error occurred while retrieving the coupon.",
                };
            }
        }

        public async Task<ResponseData<object>> GetCouponProducts(Guid id)
        {
            if (id == Guid.Empty)
                return new ResponseData<object>
                {
                    IsSuccess = false,
                    Message = "Coupon ID cannot be null or empty",
                };

            try
            {
                var coupon = await _context.Coupons
                    .Include(c => c.CouponUsers).ThenInclude(x => x.User)
                    .Include(c => c.CouponProducts).ThenInclude(x => x.Product)
                    .Where(x => x.Id == id).FirstOrDefaultAsync();

                if (coupon == null)
                    throw new KeyNotFoundException("Coupon not found");

                return new ResponseData<object>
                {
                    IsSuccess = true,
                    Message = "Coupon Fetched Successfully",
                    Data = coupon.CouponProducts,
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<object>
                {
                    IsSuccess = false,
                    Message = "An error occurred while retrieving the coupon.",
                };
            }
        }

        public async Task<ResponseData<object>> Create(CreateCouponDto dto)
        {
            if (dto == null)
               return new ResponseData<object>
            {
                IsSuccess = false,
                Message = "dto is null ",
            };

            try
            {
                 var newCoupon = new Coupon
                {
                    Code = dto.Code,
                    Name = dto.Name,
                    MinimumOrderPrice = dto.MinimumOrderPrice,
                    UseCount = dto.UseCount,
                    Value = dto.Value,
                    Type = dto.Type,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    FreeShipping = dto.FreeShipping,
                    CODNotIncluded = dto.CODNotIncluded,
                    CreatedBy = "Dynamic user"  // You can modify this to a dynamic value based on your needs
                };
                 await _context.Coupons.AddAsync(newCoupon);

                if (dto.CouponUsers.Any() && dto.SelectedProductIds.Any())
                {
                    await _context.SaveChangesAsync();
                    dto.SelectedProductIds.Distinct();
                    var couponUsers = dto.CouponUsers.Select(user => new CouponUser
                    {
                        CouponId = newCoupon.Id,
                        UserId = user.UserId,
                        Usage = user.Usage,
                    }).ToList();

                     var couponProducts = dto.SelectedProductIds.Select(productId => new CouponProduct
                    {
                        CouponId = newCoupon.Id,
                        ProductId = productId,
                    }).ToList();

                    await _context.CouponUsers.AddRangeAsync(couponUsers);
                    await _context.CouponProducts.AddRangeAsync(couponProducts);
                    await _context.SaveChangesAsync();

                    return new ResponseData<object>
                    {
                        IsSuccess = true,
                        Data = newCoupon,
                        Message = "Coupon Created Successfully ",

                    };
                }
                else
                { 
                    return new ResponseData<object>
                    {
                        IsSuccess = false,
                        Data = newCoupon,
                        Message = "You must select at least one user and one product for this coupon.",
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseData<object>
                {
                    IsSuccess = false,
                    Message = "An error occurred while creating the coupon.",
                };
            }
        }

        [HttpPost]
        public async Task<ResponseData<object>> Update(Guid id, UpdateCouponDto dto)
        {
            if (dto == null)
                return new ResponseData<object>
                {
                    IsSuccess = false,
                    Message = "  DTO cannot be null or empty",
                };

            if (id == Guid.Empty)
                return new ResponseData<object>
                {
                    IsSuccess = false,
                    Message = "Coupon ID cannot be null or empty",
                };


            try
            {
                var coupon = await _context.Coupons
                    .Include(c => c.CouponUsers).ThenInclude(x=>x.User)
                    .Include(c => c.CouponProducts).ThenInclude(x => x.Product)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (coupon == null)
                    throw new KeyNotFoundException("Coupon not found");

                // Update fields with values from the DTO
                coupon.Code = dto.Code;
                coupon.Name = dto.Name;
                coupon.MinimumOrderPrice = dto.MinimumOrderPrice;
                coupon.UseCount = dto.UseCount;
                coupon.Value = dto.Value;
                coupon.Type = dto.Type;
                coupon.StartDate = dto.StartDate;
                coupon.EndDate = dto.EndDate;
                coupon.FreeShipping = dto.FreeShipping;
                coupon.CODNotIncluded = dto.CODNotIncluded;

                if (dto.CouponUsers.Any() && dto.SelectedProductIds.Any())
                {

                    if (dto.CouponUsers != null)
                    {
                        coupon.CouponUsers = dto.CouponUsers.Select(u => new CouponUser
                        {
                            UserId = u.UserId,
                            Usage = u.Usage,
                            CouponId = coupon.Id
                        }).ToList();
                    }

                    if (dto.SelectedProductIds != null)
                    {
                        coupon.CouponProducts = dto.SelectedProductIds.Select(p => new CouponProduct
                        {
                            ProductId = p,
                            CouponId = coupon.Id
                        }).ToList();
                    }
                }

                await _context.SaveChangesAsync();
                return new ResponseData<object>
                {
                    IsSuccess = true,
                    Data =coupon ,
                    Message = "Coupon ID cannot be null or empty",
                };
            }
            catch (Exception ex)
            { 
                return new ResponseData<object>
                {
                    IsSuccess = false,
                    Message = "An error occurred while updating the coupon.",
                };
            }
        }
        public async Task<ResponseData<object>> Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Coupon ID cannot be null or empty", nameof(id));

            try
            {
                var coupon = await _context.Coupons
                    .Include(c => c.CouponUsers)
                    .Include(c => c.CouponProducts)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (coupon == null)
                    throw new KeyNotFoundException("Coupon not found");

                _context.CouponUsers.RemoveRange(coupon.CouponUsers);
                _context.CouponProducts.RemoveRange(coupon.CouponProducts);

                _context.Coupons.Remove(coupon);
                await _context.SaveChangesAsync();

               
                return new ResponseData<object>
                {
                    IsSuccess = true,
                    Message = "Coupon Deleted Successfully",
                };

            }
            catch (Exception ex)
            {
                return new ResponseData<object>
                {
                    IsSuccess = false,
                    Message = "   An error occurred while deleting the coupon.",
                };
            }
        }

      }
}
