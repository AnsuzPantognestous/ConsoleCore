using AnsuzNet.Utility.ConsoleCore.Abstractions;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore
{
    internal class ModuleMapping
    {
        private readonly Dictionary<string, ModuleInfo> mapping;
        private readonly IServiceProvider provider;

        public ModuleMapping(IServiceProvider provider)
        {
            this.provider = provider;

            var map = from type in ModuleAttribute.GetModuleTypes(Assembly.GetExecutingAssembly())
                      where provider.GetService(type) != null
                      let attr = type.GetCustomAttribute<ModuleAttribute>()
                      let Info = new ModuleInfo(type)
                      from keyWord in attr.KeyWord
                      select (keyWord, Info);
            var duplicated = from tuple in map
                             group tuple.Info.ModuleType by tuple.keyWord into tupleGroup
                             where tupleGroup.Count() > 1
                             select tupleGroup;
            var first = duplicated.FirstOrDefault();
            if (first != default)
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendLine("Multiple modules are using the same key word.")
                    .AppendLine($"KeyWord: {first.Key}")
                    .AppendLine("Modules: ");

                foreach (Type type in first)
                {
                    messageBuilder.AppendLine(type.FullName);
                }

                throw new DuplicateKeyWordException(messageBuilder.ToString());
            }
            mapping = map.ToDictionary((tuple) => tuple.keyWord, (tuple) => tuple.Info);
        }

        public ModuleMethodInfo[] GetAllMethodInfos(string moduleKeyWord)
        {
            ModuleInfo module = GetModule(moduleKeyWord);
            return module?.ModuleMethods.GetAllMethods();
        }

        public ModuleInfo GetModule(string moduleKeyWord)
        {
            if (mapping.ContainsKey(moduleKeyWord))
            {
                return mapping[moduleKeyWord];
            }
            return null;
        }

        internal ModuleMethodInfo GetMethodByKeyWord(string moduleKeyWord, string MethodKeyWord = null)
        {
            ModuleInfo module = GetModule(moduleKeyWord);
            return module?.ModuleMethods?[MethodKeyWord ?? module?.Attribute.DefaultMethodKeyWord];
        }

        internal void InvokeMethod(string moduleKeyWord, string methodKeyWord = null, IEnumerable<string> arg = null)
        {
            ModuleInfo moduleInfo = GetModule(moduleKeyWord);
            if (moduleInfo == null)
            {
                ShowModuleHelps();
            }
            ParserResult<object> result = arg == null ? null : 
                Parser.Default.ParseArguments(arg, moduleInfo.ModuleMethods[moduleKeyWord]?.Attribute?.OptionType);
            object instance = provider.GetService(mapping[moduleKeyWord].ModuleType);
            bool hadError = result?.MapResult((arg) => {
                return !moduleInfo.InvokeMethod(
                                instance, arg, methodKeyWord);
            },
            (_) => true) ?? true;
            if (hadError)
            {
                moduleInfo.ShowMethodHelps();
            }
        }

        internal void ShowModuleHelps()
        {
            foreach (var item in mapping)
            {
                string desc = item.Value.Attribute?.Description;
                if (desc != null)
                {
                    Console.WriteLine($"{item.Key}: {desc}");
                }
            }
        }
    }
}
