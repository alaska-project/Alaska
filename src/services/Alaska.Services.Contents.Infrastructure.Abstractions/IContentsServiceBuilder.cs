﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Infrastructure.Abstractions
{
    public interface IContentsServiceBuilder
    {
        IServiceCollection Services { get; }
        IConfiguration Configuration { get; }
    }
}
