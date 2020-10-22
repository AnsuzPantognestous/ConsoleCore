using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore.Abstractions
{
    public interface IStartup
    {
        void Configure(IServiceCollection collection);
    }
}
