namespace PaymentGateway.Application.Features.BackgroundJob.Implementation
{
    using System;
    using System.Linq.Expressions;

    using global::Hangfire;
    using global::Hangfire.Annotations;

    using Interfaces;

    public class BackgroundService : IBackgroundService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public BackgroundService(IBackgroundJobClient client)
        {
            this._backgroundJobClient = client;
        }

        public void Enqueue([NotNull] Expression<Action> methodCall) =>
            this._backgroundJobClient.Enqueue(methodCall);
    }
}
