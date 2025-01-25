using CouponSystem.DataAccess.Data;
using CouponSystem.Models.Dtos.ProductDto;
using CouponSystem.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace CouponSystem.Controllers
{
    //[Authorize("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _context;

        public ProductsController(IProductService productService, ApplicationDbContext context)
        {
            _productService = productService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAll(1, 10);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest("Failed to retrieve products");
        }

        [HttpGet]
        public async Task<IActionResult> GetDetails(Guid id)
        {
            var response = await _productService.Get(id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest("Product not found");
        }

        public IActionResult Create()
        {
            return PartialView("_CreatePartial");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var response = await _productService.Create(dto);
            if (response != null)
            {
                return Ok(new { success = true, message = "Product created successfully"   });
            }
            return BadRequest("Failed to create product");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.id = id;
            return PartialView("_EditPartial");
        }

        [HttpPut]
        public async Task<IActionResult> Edit( UpdateProductDto dto, Guid id)
        {
            var response = await _productService.Update(dto, id);
            if (response != null)
            {
                return Ok(new { success = true, message = "Product Updated successfully" });
            }
            return BadRequest("Failed to update product");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _productService.Delete(id);
            if (response)
            {
                return Ok("Product deleted successfully");
            }
            return BadRequest("Failed to delete product");
        }
    }
}
