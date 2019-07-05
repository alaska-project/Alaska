using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Common.Diagnostics
{
    internal class ProfilerInstance : IDisposable
    {
        private readonly ILogger logger;
        private readonly string profilerName;
        private DateTime _start = DateTime.Now;

        public ProfilerInstance(ILogger logger, string profilerName)
        {
            this.logger = logger;
            this.profilerName = profilerName;
        }

        public void Dispose()
        {
            var end = DateTime.Now;
            logger.LogTrace($"{profilerName} Profiler -> Elapsed: {end - _start}");
        }
    }
}
