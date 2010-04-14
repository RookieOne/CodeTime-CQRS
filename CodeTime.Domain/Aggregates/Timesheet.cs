using System;
using CodeTime.Domain.Commands;
using CodeTime.Domain.Events;
using Cqrs.Framework.Aggregates;
using Cqrs.Framework.Commands;
using Cqrs.Framework.Events;

namespace CodeTime.Domain.Aggregates
{
    public class TimeSheet : Aggregate,
                             ICommandHandler<CreateTimeSheet>,
                             IEventHandler<TimeSheetCreated>
    {
        DateTime _endDate;
        DateTime _startDate;

        public void Execute(CreateTimeSheet command)
        {
            Apply(new TimeSheetCreated(Id, command.StartDate));
        }

        public void On(TimeSheetCreated domainEvent)
        {
            Id = domainEvent.AggregateId;
            _startDate = domainEvent.StartDate;
        }
    }
}