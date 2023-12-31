﻿namespace MassTransit.Clients
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;


    public class ClientFactory :
        IClientFactory,
        IAsyncDisposable
    {
        public ClientFactory(ClientFactoryContext context)
        {
            Context = context;
        }

        public ValueTask DisposeAsync()
        {
            if (Context is IAsyncDisposable asyncDisposable)
                return asyncDisposable.DisposeAsync();

            return default;
        }

        public ClientFactoryContext Context { get; }

        public RequestHandle<T> CreateRequest<T>(T message, CancellationToken cancellationToken, RequestTimeout timeout)
            where T : class
        {
            IRequestClient<T> client = CreateRequestClient<T>(timeout);

            return client.Create(message, cancellationToken);
        }

        public RequestHandle<T> CreateRequest<T>(Uri destinationAddress, T message, CancellationToken cancellationToken, RequestTimeout timeout)
            where T : class
        {
            IRequestClient<T> client = CreateRequestClient<T>(destinationAddress, timeout);

            return client.Create(message, cancellationToken);
        }

        public RequestHandle<T> CreateRequest<T>(ConsumeContext consumeContext, T message, CancellationToken cancellationToken, RequestTimeout timeout)
            where T : class
        {
            IRequestClient<T> client = CreateRequestClient<T>(consumeContext, timeout);

            return client.Create(message, cancellationToken);
        }

        public RequestHandle<T> CreateRequest<T>(ConsumeContext consumeContext, Uri destinationAddress, T message, CancellationToken cancellationToken,
            RequestTimeout timeout)
            where T : class
        {
            IRequestClient<T> client = CreateRequestClient<T>(consumeContext, destinationAddress, timeout);

            return client.Create(message, cancellationToken);
        }

        public RequestHandle<T> CreateRequest<T>(object values, CancellationToken cancellationToken, RequestTimeout timeout)
            where T : class
        {
            IRequestClient<T> client = CreateRequestClient<T>(timeout);

            return client.Create(values, cancellationToken);
        }

        public RequestHandle<T> CreateRequest<T>(Uri destinationAddress, object values, CancellationToken cancellationToken, RequestTimeout timeout)
            where T : class
        {
            IRequestClient<T> client = CreateRequestClient<T>(destinationAddress, timeout);

            return client.Create(values, cancellationToken);
        }

        public RequestHandle<T> CreateRequest<T>(ConsumeContext consumeContext, object values, CancellationToken cancellationToken, RequestTimeout timeout)
            where T : class
        {
            IRequestClient<T> client = CreateRequestClient<T>(consumeContext, timeout);

            return client.Create(values, cancellationToken);
        }

        public RequestHandle<T> CreateRequest<T>(ConsumeContext consumeContext, Uri destinationAddress, object values, CancellationToken cancellationToken,
            RequestTimeout timeout)
            where T : class
        {
            IRequestClient<T> client = CreateRequestClient<T>(consumeContext, destinationAddress, timeout);

            return client.Create(values, cancellationToken);
        }

        public IRequestClient<T> CreateRequestClient<T>(RequestTimeout timeout)
            where T : class
        {
            if (EndpointConvention.TryGetDestinationAddress<T>(out var destinationAddress))
                return CreateRequestClient<T>(destinationAddress, timeout);

            return new RequestClient<T>(Context, Context.GetRequestEndpoint<T>(), timeout.Or(Context.DefaultTimeout));
        }

        public IRequestClient<T> CreateRequestClient<T>(ConsumeContext consumeContext, RequestTimeout timeout)
            where T : class
        {
            if (EndpointConvention.TryGetDestinationAddress<T>(out var destinationAddress))
                return CreateRequestClient<T>(consumeContext, destinationAddress, timeout);

            return new RequestClient<T>(Context, Context.GetRequestEndpoint<T>(consumeContext), timeout.Or(Context.DefaultTimeout));
        }

        public IRequestClient<T> CreateRequestClient<T>(Uri destinationAddress, RequestTimeout timeout)
            where T : class
        {
            IRequestSendEndpoint<T> requestSendEndpoint = Context.GetRequestEndpoint<T>(destinationAddress);

            return new RequestClient<T>(Context, requestSendEndpoint, timeout.Or(Context.DefaultTimeout));
        }

        public IRequestClient<T> CreateRequestClient<T>(ConsumeContext consumeContext, Uri destinationAddress, RequestTimeout timeout)
            where T : class
        {
            return new RequestClient<T>(Context, Context.GetRequestEndpoint<T>(destinationAddress, consumeContext), timeout.Or(Context.DefaultTimeout));
        }
    }
}
