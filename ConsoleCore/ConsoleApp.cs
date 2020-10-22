using AnsuzNet.Utility.ConsoleCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore
{
    internal class ConsoleApp : IConsoleApp
    {
        public IServiceProvider Provider { get; internal set; }
    }
}
