using System;
using System.Collections.Generic;
using System.Linq;
using Cqrs.Framework.Events;
using Cqrs.Framework.Tests.Mocks;
using NUnit.Framework;
using Rhino.Mocks;
using TestUtilities;

namespace Cqrs.Framework.Tests.Events.UnitOfWorkTests
{
    [TestFixture]
    public class get_by_id
    {
        [Test]
        public void if_no_events_found_then_should_throw_aggregate_not_found_exception()
        {
            Guid id = Guid.NewGuid();
            var mock = new MockRepository();
            var eventStore = mock.Stub<IEventStore>();
            Expect.Call(eventStore.GetAllEvents(id))
                .Return(new List<IDomainEvent>());

            mock.ReplayAll();

            var unitOfWork = new UnitOfWork(eventStore);

            Assert.Throws<AggregateNotFoundException>(() => unitOfWork.GetById<MockAggregate>(id));
        }

        [Test]
        public void should_call_load_history_on_aggregate()
        {
            Guid id = Guid.NewGuid();
            var events = new List<IDomainEvent>
                             {
                                 new MockEvent(),
                                 new MockEvent()
                             };
            var mock = new MockRepository();
            var eventStore = mock.Stub<IEventStore>();
            Expect.Call(eventStore.GetAllEvents(id))
                .Return(events);

            mock.ReplayAll();

            var unitOfWork = new UnitOfWork(eventStore);

            var aggregate = unitOfWork.GetById<MockAggregate>(id);

            aggregate.History.Count().ShouldBe(2);
        }
    }
}