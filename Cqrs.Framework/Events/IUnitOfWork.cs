using System;
using Cqrs.Framework.Aggregates;

namespace Cqrs.Framework.Events
{
    public interface IUnitOfWork
    {
        T GetById<T>(Guid id) where T : IAggregate, new();
        void Register(IAggregate aggregate);
        void Commit();
    }
}