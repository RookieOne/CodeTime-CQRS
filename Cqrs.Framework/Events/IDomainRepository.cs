using System;
using Cqrs.Framework.Aggregates;

namespace Cqrs.Framework.Events
{
    public interface IDomainRepository
    {
        TAggregate GetById<TAggregate>(Guid id) where TAggregate : IAggregate;

        void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : IAggregate;
    }
}