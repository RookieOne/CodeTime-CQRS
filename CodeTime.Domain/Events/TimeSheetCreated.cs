using System;
using Cqrs.Framework.Events;

namespace CodeTime.Domain.Events
{
    public class TimeSheetCreated : DomainEvent
    {
        public TimeSheetCreated(Guid aggregateId, DateTime startDate)
        {
            AggregateId = aggregateId;
            StartDate = startDate;
        }

        public DateTime StartDate { get; set; }
    }
}