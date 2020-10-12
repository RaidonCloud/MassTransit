namespace MassTransit
{
    using System;
    using Definition;
    using Registration;


    public interface IBusRegistrationContext :
        IRegistration
    {
        void UseHealthCheck(IBusFactoryConfigurator configurator);

        /// <summary>
        /// Configure the endpoints for all defined consumer, saga, and activity types using an optional
        /// endpoint name formatter. If no endpoint name formatter is specified and an <see cref="IEndpointNameFormatter" />
        /// is registered in the container, it is resolved from the container. Otherwise, the <see cref="DefaultEndpointNameFormatter" />
        /// is used.
        /// </summary>
        /// <param name="configurator">The <see cref="IBusFactoryConfigurator" /> for the bus being configured</param>
        /// <param name="endpointNameFormatter">Optional, the endpoint name formatter</param>
        /// <typeparam name="T">The bus factory type (depends upon the transport)</typeparam>
        void ConfigureEndpoints<T>(IReceiveConfigurator<T> configurator, IEndpointNameFormatter endpointNameFormatter)
            where T : IReceiveEndpointConfigurator;

        /// <summary>
        /// Configure the endpoints for all defined consumer, saga, and activity types using an optional
        /// endpoint name formatter. If no endpoint name formatter is specified and an <see cref="IEndpointNameFormatter" />
        /// is registered in the container, it is resolved from the container. Otherwise, the <see cref="DefaultEndpointNameFormatter" />
        /// is used.
        /// </summary>
        /// <param name="configurator">The <see cref="IBusFactoryConfigurator" /> for the bus being configured</param>
        /// <param name="endpointNameFormatter">Optional, the endpoint name formatter</param>
        /// <param name="configureFilter">A filter for the endpoints to be configured</param>
        /// <typeparam name="T">The bus factory type (depends upon the transport)</typeparam>
        void ConfigureEndpoints<T>(IReceiveConfigurator<T> configurator, IEndpointNameFormatter endpointNameFormatter,
            Action<IRegistrationFilterConfigurator> configureFilter)
            where T : IReceiveEndpointConfigurator;
    }
}