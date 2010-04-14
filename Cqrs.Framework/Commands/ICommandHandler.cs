namespace Cqrs.Framework.Commands
{
    public interface ICommandHandler<T> where T : IDomainCommand
    {
        void Execute(T command);
    }
}