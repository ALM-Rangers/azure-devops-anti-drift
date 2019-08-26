namespace Rangers.Antidrift.Drift
{
    using Castle.DynamicProxy;
    using Polly;

    public class PollyInterceptor : IInterceptor
    {
        private readonly ISyncPolicy policy;

        public PollyInterceptor(ISyncPolicy policy)
        {
            this.policy = policy;
        }

        public void Intercept(IInvocation invocation)
        {
            policy.Execute(invocation.Proceed);
        }
    }
}