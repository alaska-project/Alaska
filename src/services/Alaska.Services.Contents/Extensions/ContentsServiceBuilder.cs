using Alaska.Services.Contents.Infrastructure.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Extensions
{
    internal class ContentsServiceBuilder : IContentsServiceBuilder
    {
        public ContentsServiceBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
