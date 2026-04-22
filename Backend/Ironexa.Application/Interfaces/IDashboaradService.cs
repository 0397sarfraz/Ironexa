using Ironexa.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Application.Interfaces
{
    public interface IDashboaradService
    {
        Task<DashboardDataDto> GetDashboardSummary();
    }
}
