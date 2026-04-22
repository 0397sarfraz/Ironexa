using Ironexa.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ironexa.Infrastructure.Extensions
{
    public static class SeedExtensions
    {
        public static async Task SeedDataAsync(this IServiceProvider serviceProvider)
        {
            await IdentitySeeder.SeedAsync(serviceProvider);
        }
    }
}
