using AnsuzNet.Utility.ConsoleCore.Abstractions;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore.Extensions
{
    public static class ConsoleAppExtensions
    {
        public static void Run(this IConsoleApp app)
        {
            if (app == null)
            {
                return;
            }

            ModuleMapping modules = new ModuleMapping(app.Provider);

            IInputArgSplitter
                inputArgSplitter = (IInputArgSplitter)app.Provider.GetService(typeof(IInputArgSplitter))
                    ?? throw new Exception($"{nameof(IInputArgSplitter)} service should be add to the service collection!");

            while (true)
            {
                Functioning(inputArgSplitter, modules);
            }
        }

        private static void Functioning(IInputArgSplitter inputArgSplitter, ModuleMapping modules)
        {
            Queue<string> inputs = new Queue<string>(inputArgSplitter.Split(Console.ReadLine()));
            switch (inputs.Count)
            {
                case 0:
                    modules.ShowModuleHelps();
                    break;
                case 1:
                    modules.InvokeMethod(inputs.Dequeue());
                    break;
                case 2:
                    modules.InvokeMethod(inputs.Dequeue(), inputs.Dequeue());
                    break;
                default:
                    modules.InvokeMethod(inputs.Dequeue(), inputs.Dequeue(), inputs);
                    break;
            }
        }
    }
}
