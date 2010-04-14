using System;
using System.Collections.Generic;
using System.Linq;
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
                   where e.EventProviderId == eventProviderId
                   orderby e.Version
                   select e.Event).ToList();
        }

        public void Save(IDomainEventProvider eventProvider)
        {
            Db4oEventProvider provider = (from Db4oEventProvider p in _database
                                          where p.Id == eventProvider.Id
                                          select p).FirstOrDefault();

            int version = 0;
            if (provider == null)
            {
                provider = new Db4oEventProvider
                               {Id = eventProvider.Id, Type = eventProvider.GetType().Name, Version = 0};
            }
            else
            {
                version = provider.Version;
            }

            IEnumerable<IDomainEvent> events = eventProvider.GetChanges();

            foreach (IDomainEvent e in events)
            {
                version++;
                var db4oEvent = new Db4oEvent
                                    {
                                        EventProviderId = eventProvider.Id,
                                        Event = e,
                                        Version = version
                                    };
                _database.Store(db4oEvent);
            }

            provider.Version = version;
            _database.Store(provider);
        }

        public void Dispose()
        {
            _database.Close();
        }
    }
}