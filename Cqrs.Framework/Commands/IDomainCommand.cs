using System;

namespace Cqrs.Framework.Commands
{
    public interface IDomainCommand
    {
        Guid Id { get; }
    }
}