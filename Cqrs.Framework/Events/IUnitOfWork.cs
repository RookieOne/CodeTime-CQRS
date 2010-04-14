namespace Cqrs.Framework.Events
{
    public interface IUnitOfWork
    {
        void Register(IDomainEventProvider provider);
        void Commit();
    }
}