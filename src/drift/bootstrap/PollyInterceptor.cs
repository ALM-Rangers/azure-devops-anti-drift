// -----------------------------------------------------------------------
// <copyright file="PollyInterceptor.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace Rangers.Antidrift.Drift
{
    using Castle.DynamicProxy;
    using Polly;
    using System;

    public class PollyInterceptor : IInterceptor
    {
        private readonly ISyncPolicy policy;

        public PollyInterceptor(ISyncPolicy policy)
        {
            this.policy = policy;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation == null)
            {
                throw new ArgumentNullException(nameof(invocation));
            }

            this.policy.Execute(invocation.Proceed);
        }
    }
}