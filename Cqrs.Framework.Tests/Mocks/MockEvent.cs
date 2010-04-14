using System;
using Cqrs.Framework.Events;

namespace Cqrs.Framework.Tests.Mocks
{
    public class MockEvent : IDomainEvent
    {
        public MockEvent()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid AggregateId { get; set; }

        public int Version { get; set; }
    }
}