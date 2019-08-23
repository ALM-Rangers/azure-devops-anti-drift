using System;
using Autofac;
using Castle.DynamicProxy;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Polly;
using Rangers.Antidrift.Drift.Core;
using Rangers.Antidrift.Drift.Core.Services;

namespace Rangers.Antidrift.Drift
{
   public class Container
   {
       public static IContainer Build(VssConnection connection)
       {
            var builder = new ContainerBuilder();

            var policy = Policy.Handle<Exception>()
                               .WaitAndRetry(
                                   new[] {
                                       TimeSpan.FromSeconds(1),
                                       TimeSpan.FromSeconds(2),
                                       TimeSpan.FromSeconds(4)
                                   });

            builder.RegisterInstance<ISyncPolicy>(policy);
            builder.RegisterType<PollyInterceptor>()
                   .Named<IInterceptor>("polly");

            builder.RegisterType<GraphService>()
                   .WithParameter("connection", connection)
                   .As<IGraphService>();

           return builder.Build();
       }
   }
}