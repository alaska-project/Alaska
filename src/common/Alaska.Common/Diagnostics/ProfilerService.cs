using Alaska.Common.Diagnostics.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Common.Diagnostics
{
    internal class ProfilerService: IProfiler
    {
        private readonly ILogger<ProfilerService> _logger;

        public ProfilerService(ILogger<ProfilerService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IDisposable Measure(string profilerName)
        {
            return new ProfilerInstance(_logger, profilerName);
        }
    }
}
