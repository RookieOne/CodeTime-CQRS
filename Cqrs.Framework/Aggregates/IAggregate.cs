using System;
using System.Collections.Generic;
using Cqrs.Framework.Events;

namespace Cqrs.Framework.Aggregates
{
    public interface IAggregate
    {
        Guid Id { get; }
        IEnumerable<IDomainEvent> GetChanges();
        void LoadFromHistory(IEnumerable<IDomainEvent> domainEvents);
    }
}