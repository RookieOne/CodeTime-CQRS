using System;

namespace Cqrs.Framework.Events
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        Guid AggregateId { get; set; }        
    }
}