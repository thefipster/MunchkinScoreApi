using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using TheFipster.Munchkin.Api.Common.Exceptions;

namespace TheFipster.Munchkin.Api.Common
{
    public static class InjectionExtension
    {
        public static void AddDecoration<TInterface, TDecorator>(this IServiceCollection services)
            where TInterface : class
            where TDecorator : class, TInterface
        {
            var component = tryFindComponent<TInterface>(services);
            var ctorParams = new[] { typeof(TInterface) };
            var decorationFactory = ActivatorUtilities.CreateFactory(typeof(TDecorator), ctorParams);
            var replacement = describeReplacement<TInterface>(component, decorationFactory);
            services.Replace(replacement);
        }

        private static ServiceDescriptor describeReplacement<TInterface>(
            ServiceDescriptor component,
            ObjectFactory decorationFactory
        )
        where TInterface : class
        {
            var decoratedComponent = decorateComponent<TInterface>(component, decorationFactory);
            return ServiceDescriptor.Describe(
                typeof(TInterface),
                decoratedComponent,
                component.Lifetime);
        }

        private static Func<IServiceProvider, object> decorateComponent<TInterface>(
            ServiceDescriptor component,
            ObjectFactory decorationFactory
        )
        where TInterface : class =>
            provider => (TInterface)decorationFactory(
                provider,
                new[] { provider.CreateInstance(component) });

        private static ServiceDescriptor tryFindComponent<TInterface>(IServiceCollection services)
            where TInterface : class
        {
            try
            {
                return services.First(component => component.ServiceType == typeof(TInterface));
            }
            catch (ArgumentNullException)
            {
                throw new DecoratedComponentMissingException(typeof(TInterface));
            }

        }

        private static object CreateInstance(this IServiceProvider services, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
                return descriptor.ImplementationInstance;

            if (descriptor.ImplementationFactory != null)
                return descriptor.ImplementationFactory(services);

            return ActivatorUtilities.GetServiceOrCreateInstance(services, descriptor.ImplementationType);
        }
    }
}