using System;
using System.Collections.Generic;
using System.Linq;
using Cqrs.Framework.Events;

namespace Cqrs.Framework.Aggregates
{
    public class Aggregate : IAggregate
    {
        public Aggregate()
        {
            Id = Guid.NewGuid();
            _appliedEvents = new List<IDomainEvent>();
        }

        readonly List<IDomainEvent> _appliedEvents;

        public Guid Id { get; protected set; }

        public IEnumerable<IDomainEvent> GetChanges()
        {
            return _appliedEvents.ToList();
        }

        public void LoadFromHistory(IEnumerable<IDomainEvent> domainEvents)
        {
            if (domainEvents.Count() == 0)
                return;

            foreach(var domainEvent in domainEvents)
            {
                ApplyDomainEvent(domainEvent);
            }
        }

        void ApplyDomainEvent<T>(T domainEvent) where T : IDomainEvent
        {
            var handler = this as IEventHandler<T>;
            handler.On(domainEvent);
        }

        public void Apply<T>(T domainEvent) where T : IDomainEvent
        {
            _appliedEvents.Add(domainEvent);

            ApplyDomainEvent(domainEvent);
        }
    }
}