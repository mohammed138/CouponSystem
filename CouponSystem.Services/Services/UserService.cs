using CouponSystem.DataAccess.Data;
using CouponSystem.Models.Dtos.Helpers;
using CouponSystem.Models.Dtos.UserDto;
using CouponSystem.Models.Entities;
using CouponSystem.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponSystem.Services.Services
{
   
    public interface IUserService
    {
        Task<ResponseData<List<UserViewModel>>> GetAll();
        Task<ResponseData<UserViewModel>> Get(string id);
        Task<ResponseData<UserViewModel>> Create(CreateUserDto dto);
        Task<ResponseData<UserViewModel>> Update(UpdateUserDto dto, string userId);
        Task<ResponseData<bool>> Delete(string userId);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ResponseData<List<UserViewModel>>> GetAll()
        {
            var users = await _context.Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                EmailAddress = user.Email,
                FullName = user.FullName
            }).ToListAsync();
            return new ResponseData<List<UserViewModel>> { Data = users };
        }

        public async Task<ResponseData<UserViewModel>> Get(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return new ResponseData<UserViewModel> { IsSuccess = false, Message = "User ID cannot be null or empty" };

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ResponseData<UserViewModel> { IsSuccess = false, Message = "User not found" };

            return new ResponseData<UserViewModel>
            {
                Data = new UserViewModel
                {
                    Id = user.Id,
                    EmailAddress = user.Email,
                    FullName = user.FullName
                }
            };
        }

        public async Task<ResponseData<UserViewModel>> Create(CreateUserDto dto)
        {
            if (dto == null)
                return new ResponseData<UserViewModel> { IsSuccess = false, Message = "Invalid data" };

            var newUser = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.Email.Split('@')[0],
                FullName = dto.FullName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, dto.Password);
            if (!result.Succeeded)
            {
                return new ResponseData<UserViewModel> { IsSuccess = false, Message = string.Join(", ", result.Errors.Select(e => e.Description)) };
            }

            return new ResponseData<UserViewModel>
            {
                Data = new UserViewModel
                {
                    Id = newUser.Id,
                    EmailAddress = newUser.Email,
                    FullName = newUser.FullName
                }
            };
        }

        public async Task<ResponseData<UserViewModel>> Update(UpdateUserDto dto, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ResponseData<UserViewModel> { IsSuccess = false, Message = "User not found" };

            user.FullName = dto.FullName;
            user.Email = dto.Email;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new ResponseData<UserViewModel>
            {
                Data = new UserViewModel
                {
                    Id = user.Id,
                    EmailAddress = user.Email,
                    FullName = user.FullName
                }
            };
        }

        public async Task<ResponseData<bool>> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ResponseData<bool> { IsSuccess = false, Message = "User not found", Data = false };

            var result = await _userManager.DeleteAsync(user);
            return new ResponseData<bool> { Data = result.Succeeded };
        }
    }
}
