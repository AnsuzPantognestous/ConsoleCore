using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore
{
    internal class ModuleMethodMapping
    {
        private readonly Dictionary<string, ModuleMethodInfo> methodDict;
        
        public ModuleMethodMapping(Type moduleType)
        {
            var methods = from method in moduleType.GetMethods()
                          let attr = method.GetCustomAttribute<ModuleMethodAttribute>()
                          where attr != null
                          let info = new ModuleMethodInfo(method)
                          from keyWord in attr.KeyWord
                          select (keyWord, info);

            var duplicated = from tuple in methods
                             group tuple.info by tuple.keyWord into tupleGroup
                             where tupleGroup.Count() > 1
                             select tupleGroup;
            var first = duplicated.FirstOrDefault();

            if (first != null)
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendLine("Multiple methods in same module are using same keyword.")
                .AppendLine($"Keyword: {first.Key}")
                .AppendLine("Methods: ");

                foreach (ModuleMethodInfo method in first)
                {
                    messageBuilder.AppendLine(method.MethodInfo.Name);
                }

                throw new DuplicateKeyWordException(messageBuilder.ToString());
            }

            methodDict = methods.ToDictionary((tuple) => tuple.keyWord, (tuple) => tuple.info);
        }

        public ModuleMethodInfo this[string key] 
        {
            get
            {
                if (methodDict.ContainsKey(key))
                {
                    return methodDict[key];
                }
                return null;
            }
        }

        public ModuleMethodInfo[] GetAllMethods()
        {
            return methodDict.Values.ToArray();
        }
    }
}
