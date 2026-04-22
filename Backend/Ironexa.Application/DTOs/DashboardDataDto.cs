using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Application.DTOs
{
    public class DashboardDataDto
    {
        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }
        public int CompletedOrders { get; set; }    
        public decimal TotalEarnings { get; set; }  
    }
}
