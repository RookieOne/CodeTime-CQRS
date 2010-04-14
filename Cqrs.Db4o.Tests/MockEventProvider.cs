using System;
using System.Collections.Generic;
using System.Linq;
using Cqrs.Framework.Events;

namespace Cqrs.Db4o.Tests
{
    public class MockEventProvider : IDomainEventProvider
    {
        public MockEventProvider()
        {
            Id = Guid.NewGuid();
            _events = new List<IDomainEvent>();
        }

        readonly List<IDomainEvent> _events;
        public Guid Id { get; set; }

        public IEnumerable<IDomainEvent> GetChanges()
        {
            return _events.ToList();
        }

        public void Add(IDomainEvent domainEvent)
        {
            domainEvent.AggregateId = Id;
            _events.Add(domainEvent);
        }
    }
}