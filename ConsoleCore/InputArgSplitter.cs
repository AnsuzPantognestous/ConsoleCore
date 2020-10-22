using AnsuzNet.Utility.ConsoleCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore
{
    internal class InputArgSplitter : IInputArgSplitter
    {
        public IEnumerable<string> Split(string input)
        {
            StringBuilder builder = new StringBuilder();
            bool inString = false;
            foreach (char c in input)
            {
                switch (c)
                {
                    case ' ':
                        if (inString)
                        {
                            goto default;
                        }

                        yield return builder.ToString();
                        builder = new StringBuilder();
                        break;
                    case '\"':
                        inString ^= true;
                        goto default;
                    default:
                        builder.Append(c);
                        break;
                }
            }

            yield return builder.ToString();
        }
    }
}
