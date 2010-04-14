using System;
using Cqrs.Framework.Events;

namespace Cqrs.Db4o
{
    public class Db4oEvent
    {
        public Guid EventProviderId { get; set; }
        public IDomainEvent Event { get; set; }
        public int Version { get; set; }
    }
}