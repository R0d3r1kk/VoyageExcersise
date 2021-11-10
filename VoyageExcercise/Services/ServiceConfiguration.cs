using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VoyageExcercise.Models;

namespace VoyageExcercise.Services
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddCustomMediatR(this IServiceCollection services, MediatorDescriptionOptions options)
        {
            //get startup class type
            var mediators = new List<Type> { options.StartupClassType };
            //get IRequest Type interace
            var parentRequestType = typeof(IRequest<>);
            //get INotification type interface
            var parentNtificationType = typeof(INotification);

            foreach (var item in options.Assembly)
            {
                var instances = Assembly.Load(item).GetTypes();
                foreach (var instance in instances)
                {
                    //check if the interface is beeing inherited
                    var baseInterfaces = instance.GetInterfaces();
                    if (baseInterfaces.Count() == 0 || !baseInterfaces.Any())
                        continue;

                    //check if the IRequest<T> interface is beeing inherited
                    var requestTypes = baseInterfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == parentRequestType);
                    if (requestTypes.Count() != 0 || requestTypes.Any())
                        mediators.Add(instance);

                    //check if the INotiication<T> interface is beeing inherited
                    var notificationsTypes = baseInterfaces.Where(i => i.FullName == parentNtificationType.FullName);

                    if (notificationsTypes.Count() != 0 || notificationsTypes.Any())
                        mediators.Add(instance);
                }
            }

            //add to Dependency Injection
            services.AddMediatR(mediators.ToArray());

            return services;
        }
    }
}
