using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAttribute : Attribute
    {
        public string[] KeyWord { get; }
        public string Description { get; }
        public string DefaultMethodKeyWord { get; }

        public ModuleAttribute(string description, string defaultMethodKeyWord = null, params string[] keyWord)
        {
            KeyWord = keyWord;
            Description = description;
            DefaultMethodKeyWord = defaultMethodKeyWord;
        }

        public static IEnumerable<Type> GetModuleTypes(params Assembly[] additionalAssems)
        {
            return from type in Assembly.GetEntryAssembly().GetTypes()
                           .Concat(additionalAssems?.SelectMany((assem) => assem.GetTypes()) ?? new Type[0]).Distinct()
            let attr = type.GetCustomAttribute<ModuleAttribute>()
            where attr != null
            select type;
        }
    }
}
