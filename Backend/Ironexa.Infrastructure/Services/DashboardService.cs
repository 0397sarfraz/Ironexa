using Ironexa.Application.DTOs;
using Ironexa.Application.Interfaces;
using Ironexa.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Infrastructure.Services
{
    public class DashboardService(AppDbContext _context): IDashboaradService
    {

        public async Task<DashboardDataDto> GetDashboardSummary()
        {
            DashboardDataDto dashboardData=new DashboardDataDto();
            var order=await _context.Orders
                .Include(o=>o.OrderItems)
                .ToListAsync();

            dashboardData.TotalOrders = order.Count;
            dashboardData.PendingOrders = order.Count(x => x.Status == "Pending");
            dashboardData.CompletedOrders = order.Count(x => x.Status == "Completed");
            dashboardData.TotalEarnings = order.Where(x => x.Status == "Completed").SelectMany(o => o.OrderItems).Sum(i => i.TotalAmount??0);

            return dashboardData;
        }
    }
}
