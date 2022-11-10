using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterCollection(this IServiceCollection services, Type openGenericServiceType, IEnumerable<Assembly> assemblies)
        {
            foreach (Type item in from assembly in assemblies.Distinct()
                                  where !assembly.IsDynamic
                                  from type in GetTypesFromAssembly(assembly)
                                  where !type.IsGenericTypeDefinition
                                  where ServiceIsAssignableFromImplementation(openGenericServiceType, type)
                                  select type)
            {
                Type serviceType = item.GetTypeInfo().ImplementedInterfaces.First();
                services.AddSingleton(serviceType, item);
            }
        }

        private static IEnumerable<Type> GetTypesFromAssembly(Assembly assembly)
        {
            return assembly.GetTypes();
        }

        private static bool ServiceIsAssignableFromImplementation(Type service, Type implementation)
        {
            if (!service.IsGenericType)
            {
                return service.IsAssignableFrom(implementation);
            }

            if (implementation.IsGenericType && implementation.GetGenericTypeDefinition() == service)
            {
                return true;
            }

            Type[] interfaces = implementation.GetInterfaces();
            for (int i = 0; i < interfaces.Length; i++)
            {
                if (IsGenericImplementationOf(interfaces[i], service))
                {
                    return true;
                }
            }

            Type type = implementation.BaseType ?? ((implementation != typeof(object)) ? typeof(object) : null);
            while (type != null)
            {
                if (IsGenericImplementationOf(type, service))
                {
                    return true;
                }

                type = type.BaseType;
            }

            return false;
        }

        private static bool IsGenericImplementationOf(Type type, Type serviceType)
        {
            if (!(type == serviceType) && !serviceType.IsVariantVersionOf(type))
            {
                if (type.IsGenericType)
                {
                    return type.GetGenericTypeDefinition() == serviceType;
                }

                return false;
            }

            return true;
        }
        private static bool IsVariantVersionOf(this Type type, Type otherType)
        {
            if (type.IsGenericType && otherType.IsGenericType && type.GetGenericTypeDefinition() == otherType.GetGenericTypeDefinition())
            {
                return type.IsAssignableFrom(otherType);
            }

            return false;
        }


    }
}
