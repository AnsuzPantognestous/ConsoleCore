using AnsuzNet.Utility.ConsoleCore.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnsuzNet.Utility.ConsoleCore
{
    internal class ConsoleAppBuilder : IConsoleAppBuilder
    {
        private event Action<IServiceCollection> OnBuild;

        private DefaultServiceProviderFactory serviceProviderFactory = new DefaultServiceProviderFactory();

        private bool hadBuilt = false;

        public IConsoleApp Build()
        {
            if (hadBuilt)
            {
                throw new InvalidOperationException("Build can only be called once.");
            }
            hadBuilt = true;

            IServiceCollection collection = new ServiceCollection();

            OnBuild?.Invoke(collection);

            return new ConsoleApp
            {
                Provider = serviceProviderFactory.CreateServiceProvider(collection)
            };
        }

        public IConsoleAppBuilder ConfigureServices(Action<IServiceCollection> configureDelegate)
        {
            OnBuild += configureDelegate;
            return this;
        }
    }
}
