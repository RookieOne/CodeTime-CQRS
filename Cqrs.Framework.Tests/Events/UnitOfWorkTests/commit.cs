using Cqrs.Framework.Events;
using Cqrs.Framework.Tests.Mocks;
using NUnit.Framework;
using Rhino.Mocks;

namespace Cqrs.Framework.Tests.Events.UnitOfWorkTests
{
    [TestFixture]
    public class commit
    {
        [Test]
        public void should_call_save_on_event_store()
        {
            var mocks = new MockRepository();
            var eventStore = mocks.DynamicMock<IEventStore>();
            var aggregate = new MockAggregate();

            Expect.Call(() => eventStore.Save(aggregate));
            mocks.ReplayAll();

            var unitOfWork = new UnitOfWork(eventStore);

            unitOfWork.Register(aggregate);

            unitOfWork.Commit();

            mocks.VerifyAll();
        }

        [Test]
        public void should_not_call_save_on_second_commit()
        {
            var mocks = new MockRepository();
            var eventStore = mocks.DynamicMock<IEventStore>();
            var aggregate = new MockAggregate();

            Expect.Call(() => eventStore.Save(aggregate));
            mocks.ReplayAll();

            var unitOfWork = new UnitOfWork(eventStore);

            unitOfWork.Register(aggregate);

            unitOfWork.Commit();
            unitOfWork.Commit();

            mocks.VerifyAll();
        }
    }
}