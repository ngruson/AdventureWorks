﻿using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace AW.Application.AutoMapper
{
    public class BaseMappingProfile : Profile
    {
        public BaseMappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes();
            var interfaceType = typeof(IMapFrom<>);
            var methodName = nameof(IMapFrom<object>.Mapping);
            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType)
                    .ToList();

                // Has the type implemented any IMapFrom<T>?
                if (interfaces.Count > 0)
                {
                    // Yes, then let's create an instance
                    var instance = Activator.CreateInstance(type);

                    // and invoke each implementation of `.Mapping()`
                    foreach (var i in interfaces)
                    {
                        var methodInfo = i.GetMethod(methodName, argumentTypes);

                        methodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}