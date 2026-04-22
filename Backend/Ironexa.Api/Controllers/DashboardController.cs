using Ironexa.Application.DTOs;
using Ironexa.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ironexa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController(IDashboaradService _dashboard) : ControllerBase
    {
        [HttpGet("summary")]
        public async Task<IActionResult> DashboardSummary()
        {
            DashboardDataDto dashboardData=new DashboardDataDto();
            try
            {
                dashboardData= await _dashboard.GetDashboardSummary();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(dashboardData);
        }
    }
}
