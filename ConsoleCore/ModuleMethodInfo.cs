using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore
{
    internal class ModuleMethodInfo
    {
        internal MethodInfo MethodInfo { get; }

        internal ModuleMethodAttribute Attribute { get; }

        internal ModuleMethodInfo(MethodInfo method)
        {
            Attribute = method.GetCustomAttribute<ModuleMethodAttribute>();
            if (Attribute == null)
            {
                throw new ArgumentException($"{nameof(method)} don't have {nameof(ModuleMethodAttribute)}.");
            }
        }

        internal void InvokeMethod(object instance, object arg)
        {
            MethodInfo.Invoke(instance, new object[]{ arg });
        }
    }
}
