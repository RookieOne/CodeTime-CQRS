using System;

namespace Cqrs.Framework.Commands
{
    public class DomainCommand : IDomainCommand
    {
        public DomainCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}