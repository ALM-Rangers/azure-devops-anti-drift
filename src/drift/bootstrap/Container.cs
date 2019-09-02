// -----------------------------------------------------------------------
// <copyright file="Container.cs" company="ALM | DevOps Rangers">
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
    using Castle.DynamicProxy;
    using Microsoft.VisualStudio.Services.WebApi;
    using Polly;
    using Rangers.Antidrift.Drift.Core;
    using Rangers.Antidrift.Drift.Core.Services;

    public class Container
    {
        public static IContainer Build(VssConnection connection)
        {
            var builder = new ContainerBuilder();

            var policy = Policy.Handle<Exception>()
                               .WaitAndRetry(
                                   new[]
                                   {
                                       TimeSpan.FromSeconds(1),
                                       TimeSpan.FromSeconds(2),
                                       TimeSpan.FromSeconds(4),
                                   });

            builder.RegisterInstance<ISyncPolicy>(policy);
            builder.RegisterType<PollyInterceptor>()
                   .Named<IInterceptor>("polly");

            builder.RegisterType<GraphService>()
                   .WithParameter("connection", connection)
                   .As<IGraphService>();
            
            builder.RegisterType<ApplicationGroup>();
            builder.RegisterType<Namespace>();
            builder.RegisterType<Organization>();
            builder.RegisterType<SecurityPattern>();
            builder.RegisterType<Team>();
            builder.RegisterType<TeamProject>();

            return builder.Build();
        }
    }
}