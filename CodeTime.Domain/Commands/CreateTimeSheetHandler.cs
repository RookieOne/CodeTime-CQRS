using CodeTime.Domain.Aggregates;
using Cqrs.Framework.Commands;
using Cqrs.Framework.Events;

namespace CodeTime.Domain.Commands
{
    public class CreateTimeSheetHandler : ICommandHandler<CreateTimeSheet>
    {
        private readonly IDomainRepository _domainRepository;

        public CreateTimeSheetHandler(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public void Execute(CreateTimeSheet command)
        {
            var timeSheet = new TimeSheet();
            var handler = timeSheet as ICommandHandler<CreateTimeSheet>;
            handler.Execute(command);

            _domainRepository.Add(timeSheet);
        }
    }
}