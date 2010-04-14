using System;
using System.Collections.Generic;
using System.Linq;
using Cqrs.Framework.Aggregates;
using Cqrs.Framework.Events;

namespace Cqrs.Framework.Tests.Mocks
{
    public class MockAggregate : IAggregate
    {
        public MockAggregate()
        {
            Id = Guid.NewGuid();
            _events = new List<IDomainEvent>();
        }

        readonly List<IDomainEvent> _events;
        public IEnumerable<IDomainEvent> History { get; private set; }
        public Guid Id { get; set; }

        public IEnumerable<IDomainEvent> GetChanges()
        {
            return _events.ToList();
        }

        public void LoadFromHistory(IEnumerable<IDomainEvent> domainEvents)
        {
            History = domainEvents;
        }

        public void Add(IDomainEvent domainEvent)
        {
            domainEvent.AggregateId = Id;
            _events.Add(domainEvent);
        }
    }
}