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
    public interface IDashboardService
    {
        Task<ResponseData<object>> GetSystemStatices();

    }
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseData<object>> GetSystemStatices()
        {
            try
            {
                var CouponCount = await _context.Coupons.CountAsync();
                var UsersCount = await _context.Users.CountAsync();
                var ProductCount = await _context.Products.CountAsync();

                var model = new
                {
                    couponCount = CouponCount,
                    UsersCount = UsersCount,
                    ProductsCount = ProductCount,

                };
                return new ResponsDataByPage<object>
                {
                    IsSuccess =true , 
                    Message = "Statices Fetched Successfully ",
                    Data = model,
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
  

      }
}
