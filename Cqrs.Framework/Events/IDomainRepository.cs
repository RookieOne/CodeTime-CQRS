using System;
using Cqrs.Framework.Aggregates;

namespace Cqrs.Framework.Events
{
    public interface IDomainRepository
    {
        T GetById<T>(Guid id) where T : IAggregate, new();

        void Add<T>(T aggregate) where T : IAggregate, new();
    }
}