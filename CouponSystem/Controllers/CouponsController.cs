using CouponSystem.DataAccess.Data;
using CouponSystem.Models.Dtos.CouponDto;
using CouponSystem.Models.ViewModels;
using CouponSystem.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CouponSystem.Controllers
{
    public class CouponsController : Controller
    {
        private readonly ICouponService _couponService;
        private readonly ApplicationDbContext _Context;

        public CouponsController(ICouponService couponService , ApplicationDbContext _Context)
        {
            _couponService = couponService;
            this._Context = _Context;
        }


        public async Task<IActionResult> Index()
        {
            return View();
        }


        public async Task<IActionResult> GetAll()
        {
            var response = await _couponService.GetAll(1, 20);
            if (response.IsSuccess)
                return Ok(response);
            return Ok(response); 
        }
        public async Task<IActionResult> GetDetails(Guid id)
        {
            var response = await _couponService.Get(id);
            if (response.IsSuccess)
                return Ok(response);
            return Ok(response);
        }




        public IActionResult Create()
        {
            ViewBag.CouponProducts = _Context.Products.Select(x => new CreateCouponProductDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            ViewBag.CouponUsers = _Context.Users.Select(x => new CreateCouponUserDto
            {
                UserId = x.Id,
                FullName = x.FullName ?? x.Email,
                Usage = 0
            }).ToList();

            return PartialView("_CreateCouponPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Create( CreateCouponDto dto)
        {
            var response = await _couponService.Create(dto);
            if (response.IsSuccess)
                return Ok(new { success = true, message = "Coupon created successfully" });
            return Ok(response); 
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.CouponId = id;
            ViewBag.CouponProducts = _Context.Products.Select(x => new CreateCouponProductDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            ViewBag.CouponUsers = _Context.Users.Select(x => new CreateCouponUserDto
            {
                UserId = x.Id,
                FullName = x.FullName ?? x.Email,
                Usage = 0
            }).ToList();

            return PartialView("_EditCouponPartial");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UpdateCouponDto dto)
        {
            var response = await _couponService.Update(id, dto);
            if (response.IsSuccess)
                return Ok(new { success = true, message = "Coupon Updated successfully" });
            return Ok(response);  
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _couponService.Delete(id);
            if (response.IsSuccess)
                return Ok(new { success = true, message = "Coupon Deleted successfully" });
            return Ok(response);  
        }



         
    }

}
