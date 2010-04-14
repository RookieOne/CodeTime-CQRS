namespace Cqrs.Framework.Commands
{
    public interface ICommandBus
    {
        void Execute(IDomainCommand command);
    }
}