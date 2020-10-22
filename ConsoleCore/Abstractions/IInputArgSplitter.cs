using System;
using System.Collections.Generic;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore.Abstractions
{
    public interface IInputArgSplitter
    {
        IEnumerable<string> Split(string input);
    }
}
