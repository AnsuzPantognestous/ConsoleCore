using AnsuzNet.Utility.ConsoleCore.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddModules(this IServiceCollection collection, params Assembly[] additionalAssems)
        {
            foreach (Type type in ModuleAttribute.GetModuleTypes(additionalAssems))
            {
                collection.AddSingleton(type);
            }
            return collection;
        }

        public static IServiceCollection AddInputArgSplitter(this IServiceCollection collection, IInputArgSplitter splitter = null)
        {
            collection.AddSingleton(splitter ?? new InputArgSplitter());
            return collection;
        }
    }
}
