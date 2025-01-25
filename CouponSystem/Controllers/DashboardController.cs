using CouponSystem.Models;
using CouponSystem.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CouponSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IDashboardService _dashboardService;
        public DashboardController(ILogger<DashboardController> logger , IDashboardService _dashboardService)
        {
            _logger = logger;
            this._dashboardService = _dashboardService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetStatices()
        {
            var res =await _dashboardService.GetSystemStatices();
            return Ok(res);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}