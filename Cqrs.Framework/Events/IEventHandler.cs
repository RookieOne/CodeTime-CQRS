namespace Cqrs.Framework.Events
{
    public interface IEventHandler<T> where T : IDomainEvent
    {
        void On(T domainEvent);
    }
}