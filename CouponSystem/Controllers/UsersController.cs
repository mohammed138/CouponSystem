using CouponSystem.DataAccess.Data;
using CouponSystem.Models.Dtos.UserDto;
using CouponSystem.Models.Entities;
using CouponSystem.Services.Services;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CouponSystem.Controllers
{
    //[Authorize("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        
        public UsersController(IUserService userService , ApplicationDbContext _context , UserManager<ApplicationUser> _userManager) { 
        
            this._userService = userService;
            this._context = _context;
            this._userManager = _userManager;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
     

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.GetAll();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetDetails(string id)
        {
            var response = await _userService.Get(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        public async Task<IActionResult> Create()
        {
           return PartialView("_CreateUserPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Create( CreateUserDto dto)
        {
            var response = await _userService.Create(dto);
            if (response.IsSuccess)
            {
                return Ok(new { success = true, message = "Users created successfully" });
            }
            return BadRequest(response);
        }


        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.id = id;
            return PartialView("_EditUserPartial");

        }
        [HttpPost]
        public async Task<IActionResult> Edit( UpdateUserDto dto, string id)
        {
            var response = await _userService.Update(dto, id);
            if (response.IsSuccess)
            {
                return Ok(new { success = true, message = "Users Updated successfully" });
            }
            return BadRequest(response);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var response = await _userService.Delete(id);
            if (response.IsSuccess)
            {
                return Ok("Users deleted successfully");
            }
            return BadRequest(response);
        }





    }
}
