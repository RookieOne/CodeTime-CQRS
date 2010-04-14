using System;
using System.Collections.Generic;
using System.Linq;
using Cqrs.Framework.Aggregates;
using Cqrs.Framework.Events;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

namespace Cqrs.Db4o
{
    public class Db4oEventStore : IEventStore
    {
        public Db4oEventStore(string fileName)
        {
            _database = Db4oEmbedded.OpenFile(Db4oEmbedded.NewConfiguration(), fileName);
        }

        readonly IObjectContainer _database;

        public IEnumerable<IDomainEvent> GetAllEvents(Guid eventProviderId)
        {
            return (from Db4oEvent e in _database
                    where e.AggregateId == eventProviderId
                    orderby e.Version
                    select e.Event).ToList();
        }

        public void Save(IAggregate aggregate)
        {
            Db4oAggregate dbAggregate = (from Db4oAggregate a in _database
                                         where a.Id == aggregate.Id
                                         select a).FirstOrDefault();

            int version = 0;
            if (dbAggregate == null)
            {
                dbAggregate = new Db4oAggregate { Id = aggregate.Id, Type = aggregate.GetType().Name, Version = 0 };
            }
            else
            {
                version = dbAggregate.Version;
            }

            IEnumerable<IDomainEvent> events = aggregate.GetChanges();

            foreach (IDomainEvent e in events)
            {
                version++;
                var db4oEvent = new Db4oEvent
                                    {
                                        AggregateId = dbAggregate.Id,
                                        Event = e,
                                        Version = version
                                    };
                _database.Store(db4oEvent);
            }

            dbAggregate.Version = version;
            _database.Store(dbAggregate);
        }

        public void Dispose()
        {
            _database.Close();
        }
    }
}