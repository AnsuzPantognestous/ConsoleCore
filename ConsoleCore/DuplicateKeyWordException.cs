using System;
using System.Collections.Generic;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore
{
    public class DuplicateKeyWordException : Exception
    {
        public DuplicateKeyWordException(string message) : base(message)
        {

        }
    }
}
