// Copyright 2007-2016 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit
{
    using System;
    using System.ComponentModel;
    using Builders;
    using Transports.InMemory;


    public interface IInMemoryBusFactoryConfigurator :
        IBusFactoryConfigurator
    {
        /// <summary>
        /// Sets the maximum number of threads used by an in-memory transport, for partitioning
        /// the input queue. This setting also specifies how many threads will be used for dispatching
        /// messages to consumers. 
        /// </summary>
        int TransportConcurrencyLimit { set; }

        /// <summary>
        /// Sets the transport provider for the InMemory bus, used to share a transport cache between multiple
        /// bus instances. Normally this method is not used.
        /// </summary>
        /// <param name="transportProvider"></param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void SetHost(InMemoryHost transportProvider);

        [EditorBrowsable(EditorBrowsableState.Never)]
        void AddBusFactorySpecification(IBusFactorySpecification<IInMemoryBusBuilder> specification);

        [EditorBrowsable(EditorBrowsableState.Never)]
        void AddReceiveEndpointSpecification(IReceiveEndpointSpecification<IInMemoryBusBuilder> specification);

        /// <summary>
        /// Specify a receive endpoint for the bus, with the specified queue name
        /// </summary>
        /// <param name="queueName">The queue name for the receiving endpoint</param>
        /// <param name="configureEndpoint">The configuration callback</param>
        void ReceiveEndpoint(string queueName, Action<IInMemoryReceiveEndpointConfigurator> configureEndpoint);
    }
}