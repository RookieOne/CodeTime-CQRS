using System;
using System.Linq;
using CodeTime.Domain.Aggregates;
using CodeTime.Domain.Commands;
using CodeTime.Domain.Events;
using Cqrs.Framework.Events;
using NUnit.Framework;
using Rhino.Mocks;
using TestUtilities;

namespace CodeTime.Domain.Tests.Timesheet
{
    [TestFixture]
    public class creating_time_sheet
    {
        TimeSheet _timeSheet;
        DateTime _startDate;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _timeSheet = null;

            var mocks = new MockRepository();
            var repository = mocks.Stub<IDomainRepository>();
            Expect.Call(() => repository.Add<TimeSheet>(null))
                .IgnoreArguments()
                .WhenCalled(i => _timeSheet = i.Arguments[0] as TimeSheet);
            mocks.ReplayAll();

            _startDate = DateTime.Today;
            var createTimeSheet = new CreateTimeSheet(_startDate);
            var handler = new CreateTimeSheetHandler(repository);
            handler.Execute(createTimeSheet);
        }

        [Test]
        public void should_add_time_sheet_to_repository()
        {
            _timeSheet.ShouldNotBe(null);
        }

        [Test]
        public void should_set_start_date()
        {
            var createdEvent = _timeSheet.GetChanges().First() as TimeSheetCreated;
            createdEvent.StartDate.ShouldBe(_startDate);
        }

        [Test]
        public void timesheet_should_have_created_event()
        {
            _timeSheet.GetChanges().Count().ShouldBe(1);
            _timeSheet.GetChanges().First().ShouldBeType<TimeSheetCreated>();
        }
    }
}