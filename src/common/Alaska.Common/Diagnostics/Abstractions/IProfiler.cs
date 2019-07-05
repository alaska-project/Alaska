using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Common.Diagnostics.Abstractions
{
    public interface IProfiler
    {
        IDisposable Measure(string profilerName);
    }
}
