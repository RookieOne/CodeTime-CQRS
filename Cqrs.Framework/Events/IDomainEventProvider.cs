using System;
using System.Collections.Generic;

namespace Cqrs.Framework.Events
{
    public interface IDomainEventProvider
    {
        Guid Id { get; }
        IEnumerable<IDomainEvent> GetChanges();
    }
}