using System;
using Cqrs.Framework.Aggregates;

namespace Cqrs.Framework.Events
{
    public class DomainRepository : IDomainRepository
    {
        public DomainRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        readonly IUnitOfWork _unitOfWork;

        public T GetById<T>(Guid id) where T : IAggregate, new()
        {
            return _unitOfWork.GetById<T>(id);
        }

        public void Add<T>(T aggregate) where T : IAggregate, new()
        {
            _unitOfWork.Register(aggregate);
        }
    }
}