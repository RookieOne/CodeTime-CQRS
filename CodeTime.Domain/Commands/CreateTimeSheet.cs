using System;
using Cqrs.Framework.Commands;

namespace CodeTime.Domain.Commands
{
    public class CreateTimeSheet : DomainCommand
    {
        public CreateTimeSheet(DateTime startDate)
        {
            StartDate = startDate;
        }

        public DateTime StartDate { get; set; }
    }
}