namespace PaymentGateway.Application.Features.BackgroundJob.Interfaces
{
    using System;
    using System.Linq.Expressions;

    using Hangfire.Annotations;

    public interface IBackgroundService
    {
        void Enqueue([NotNull] Expression<Action> methodCall);
    }
}
