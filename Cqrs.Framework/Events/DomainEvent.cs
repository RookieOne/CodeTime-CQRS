using System;

namespace Cqrs.Framework.Events
{
    public class DomainEvent : IDomainEvent
    {
        public DomainEvent()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Guid AggregateId { get; set; }        
    }
}