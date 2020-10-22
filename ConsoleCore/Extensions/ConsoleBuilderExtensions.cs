using AnsuzNet.Utility.ConsoleCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore.Extensions
{
    public static class ConsoleBuilderExtensions
    {
        public static IConsoleAppBuilder UseStartup<TStarup>(this IConsoleAppBuilder builder)
            where TStarup : IStartup, new()
        {
            TStarup startup = new TStarup();
            return builder?
                .ConfigureServices((collection) => startup.Configure(collection));
        }

        public static IConsoleAppBuilder UseInputSplitter(this IConsoleAppBuilder builder, IInputArgSplitter splitter = null)
        {
            return builder?.ConfigureServices((collection) => collection.AddInputArgSplitter(splitter));
        }
    }
}
