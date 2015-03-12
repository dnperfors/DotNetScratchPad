using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.ServiceModel.Description;

namespace WCFBowlingGameService.UnitTests
{
    /// <summary>
    /// Helper class to host WCF services for use with unit tests.
    /// </summary>
    public static class WcfServiceHost
    {
        private static readonly IDictionary<Type, ServiceHost> ServiceHosts = new Dictionary<Type, ServiceHost>();
        private static readonly IDictionary<Type, ChannelFactory> ChannelFactories = new Dictionary<Type, ChannelFactory>();

        /// <summary>
        /// Start a WCF Service and configure it to be bound to a named pipe.
        /// </summary>
        /// <typeparam name="TService">The service implementation</typeparam>
        /// <typeparam name="TInterface">The service interface</typeparam>
        public static void StartService<TService, TInterface>()
        {
            var serviceType = typeof(TService);
            var interfaceType = typeof(TInterface);

            if (ServiceHosts.ContainsKey(interfaceType)) return;

            var stringUri = @"net.pipe://localhost/" + interfaceType.Name;
            var instance = new ServiceHost(serviceType, new Uri(stringUri));
            
            NetNamedPipeBinding binding = new NetNamedPipeBinding();
            binding.Security.Mode = NetNamedPipeSecurityMode.Transport;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            instance.AddServiceEndpoint(interfaceType, binding, stringUri);
            instance.Open();
            ServiceHosts.Add(interfaceType, instance);
        }

        /// <summary>
        /// Stop a specific WCF Service and the Channel.
        /// </summary>
        /// <typeparam name="TInterface">The type of the service that should be stopped.</typeparam>
        public static void StopService<TInterface>()
        {
            var type = typeof(TInterface);
            if (!ServiceHosts.ContainsKey(type)) return;
            var instance = ServiceHosts[type];
            StopChannelFactory<TInterface>();
            if (instance.State != CommunicationState.Closed) instance.Close();
            ServiceHosts.Remove(type);
        }

        /// <summary>
        /// Stop the channel for a specific service.
        /// </summary>
        /// <typeparam name="TInterface">The type of the service from wihch the channel should be stopped.</typeparam>
        public static void StopChannelFactory<TInterface>()
        {
            var type = typeof(TInterface);
            if (!ChannelFactories.ContainsKey(type)) return;
            var instance = ChannelFactories[type];
            try
            {
                // the state is not faulted if an exception takes place
                if (instance.State != CommunicationState.Closed && instance.State != CommunicationState.Faulted) instance.Close();
            }
            catch { }
            ChannelFactories.Remove(type);
        }

        /// <summary>
        /// Perform some actions on the service.
        /// </summary>
        /// <typeparam name="TInterface">The service on which the action should be performed.</typeparam>
        /// <param name="action">The action that should be perfomed</param>
        public static void InvokeService<TInterface>(Action<TInterface> action)
        {
            var factory = GetChannelFactory<TInterface>();
            var client = factory.CreateChannel();
            action(client);
        }

        /// <summary>
        /// Creates a client to be able to communicate with a service.
        /// </summary>
        /// <typeparam name="TInterface">The service to communicate with.</typeparam>
        /// <returns></returns>
        public static ChannelFactory<TInterface> GetChannelFactory<TInterface>()
        {
            var type = typeof(TInterface);
            if (ChannelFactories.ContainsKey(type)) return ChannelFactories[type] as ChannelFactory<TInterface>;
            var netPipeService = new ServiceEndpoint(
                ContractDescription.GetContract(type),
                new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/" + type.Name));

            var factory = new ChannelFactory<TInterface>(netPipeService);
            ChannelFactories.Add(type, factory);
            return factory;
        }
    }
}
