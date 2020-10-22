using System;
using System.Collections.Generic;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ModuleMethodAttribute : Attribute
    {
        public string[] KeyWord { get; }
        public string HelpText { get; }
        public Type OptionType { get; }

        public ModuleMethodAttribute(string helpText, Type optionType, params string[] keyWord)
        {
            KeyWord = keyWord;
            HelpText = helpText;
            OptionType = optionType;
        }
    }
}
