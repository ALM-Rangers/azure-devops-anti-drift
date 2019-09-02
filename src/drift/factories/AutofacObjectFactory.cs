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
    using Autofac;
    using YamlDotNet.Serialization;

    public class AutofacObjectFactory : IObjectFactory
    {
        private readonly IComponentContext context;

        public AutofacObjectFactory(IComponentContext context)
        {
            this.context = context;
        }

        public object Create(Type type)
        {
            return this.context.Resolve(type);
        }
    }
}