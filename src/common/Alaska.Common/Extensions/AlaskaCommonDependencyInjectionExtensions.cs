using Alaska.Common.Diagnostics;
using Alaska.Common.Diagnostics.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Common.Extensions
{
    public static class AlaskaCommonDependencyInjectionExtensions
    {
        public static IServiceCollection AddAlaskaCommon(this IServiceCollection services)
        {
            return services.AddTransient<IProfiler, ProfilerService>();
        }
    }
}
