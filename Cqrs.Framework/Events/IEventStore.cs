using System;
using System.Collections.Generic;
using Cqrs.Framework.Aggregates;

namespace Cqrs.Framework.Events
{
    public interface IEventStore : IDisposable
    {
        IEnumerable<IDomainEvent> GetAllEvents(Guid aggregateId);
        void Save(IAggregate aggregate);
    }
}