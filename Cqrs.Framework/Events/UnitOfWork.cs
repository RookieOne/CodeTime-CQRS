using System;
using System.Collections.Generic;
using System.Linq;
using Cqrs.Framework.Aggregates;

namespace Cqrs.Framework.Events
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IEventStore eventStore)
        {
            _eventStore = eventStore;
            _aggregates = new List<IAggregate>();
        }

        readonly List<IAggregate> _aggregates;
        readonly IEventStore _eventStore;

        public T GetById<T>(Guid id) where T : IAggregate, new()
        {
            IEnumerable<IDomainEvent> events = _eventStore.GetAllEvents(id);

            if (events.Count() == 0)
                throw new AggregateNotFoundException(id);

            var aggregate = new T();

            aggregate.LoadFromHistory(events);

            _aggregates.Add(aggregate);

            return aggregate;
        }

        public void Register(IAggregate aggregate)
        {
            _aggregates.Add(aggregate);
        }

        public void Commit()
        {
            foreach (IAggregate aggregate in _aggregates)
            {
                _eventStore.Save(aggregate);
            }
            _aggregates.Clear();
        }
    }
}