using System;
using System.Collections.Generic;

namespace Cqrs.Framework.Events
{
    public interface IEventStore : IDisposable
    {
        IEnumerable<IDomainEvent> GetAllEvents(Guid eventProviderId);
        void Save(IDomainEventProvider eventProvider);
    }
}