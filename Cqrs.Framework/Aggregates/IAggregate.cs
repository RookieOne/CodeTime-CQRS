using System;
using Cqrs.Framework.Events;

namespace Cqrs.Framework.Aggregates
{
    public interface IAggregate : IDomainEventProvider
    {        
    }
}