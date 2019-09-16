// -----------------------------------------------------------------------
// <copyright file="AutofacObjectFactory.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------



namespace Rangers.Antidrift.Drift
{
    using System;
    using System.Collections.Generic;
    using Autofac;
    using YamlDotNet.Serialization;

    public class AutofacObjectFactory : IObjectFactory
    {
        private static readonly Dictionary<Type, Type> defaultInterfaceImplementations = new Dictionary<Type, Type>
        {
            { typeof(IEnumerable<>), typeof(List<>) },
            { typeof(ICollection<>), typeof(List<>) },
            { typeof(IList<>), typeof(List<>) },
            { typeof(IDictionary<,>), typeof(Dictionary<,>) }
        };

        private readonly IComponentContext context;

        public AutofacObjectFactory(IComponentContext context)
        {
            this.context = context;
        }

        public object Create(Type type)
        {
            if(this.context.IsRegistered(type))
            {
                return this.context.Resolve(type);
            }

            if (type.IsInterface)
            {
                Type implementationType;
                if (defaultInterfaceImplementations.TryGetValue(type.GetGenericTypeDefinition(), out implementationType))
                {
                    type = implementationType.MakeGenericType(type.GetGenericArguments());
                }
            }

            try
            {
                return Activator.CreateInstance(type);
            }
            catch (Exception err)
            {
                var message = string.Format("Failed to create an instance of type '{0}'.", type);
                throw new InvalidOperationException(message, err);
            }
        }
    }
}