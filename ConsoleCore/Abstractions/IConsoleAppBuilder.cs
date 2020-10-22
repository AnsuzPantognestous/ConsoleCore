using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore.Abstractions
{
    public interface IConsoleAppBuilder
    {
        IConsoleApp Build();

        IConsoleAppBuilder ConfigureServices(Action<IServiceCollection> configureDelegate);
    }
}
