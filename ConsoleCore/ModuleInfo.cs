using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore
{
    internal class ModuleInfo
    {
        internal ModuleMethodMapping ModuleMethods { get; }

        internal Type ModuleType { get; }

        internal ModuleAttribute Attribute { get; }

        internal ModuleInfo(Type moduleType)
        {
            ModuleMethods = new ModuleMethodMapping(moduleType);
            Attribute = moduleType.GetCustomAttribute<ModuleAttribute>();
            ModuleType = moduleType;

        }

        internal bool InvokeMethod(object instance, object arg, string methodKeyWord = null)
        {
            ModuleMethodInfo moduleMethodInfo = ModuleMethods[methodKeyWord];
            if (moduleMethodInfo == null)
            {
                return false;
            }
            moduleMethodInfo.InvokeMethod(instance, arg);
            return true;
        }

        internal void ShowMethodHelps()
        {
            foreach (var method in ModuleMethods.GetAllMethods())
            {
                Console.WriteLine($"{method.Attribute.KeyWord}: {method.Attribute.HelpText}");
            }
        }
    }
}
