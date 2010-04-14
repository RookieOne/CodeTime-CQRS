using System;

namespace Cqrs.Db4o
{
    public class Db4oEventProvider
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public int Version { get; set; }
    }
}