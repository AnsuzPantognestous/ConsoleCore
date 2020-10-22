using System;
using System.Collections.Generic;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore.Abstractions
{
    public interface IConsoleApp
    {
        IServiceProvider Provider { get; }
    }
}
