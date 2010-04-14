using System;

namespace Cqrs.Framework.Events
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(Guid id)
            : base(GetMessage(id))
        {
        }

        static string GetMessage(Guid id)
        {
            return string.Format("Aggregate with Id {0} not found.", id);
        }
    }
}