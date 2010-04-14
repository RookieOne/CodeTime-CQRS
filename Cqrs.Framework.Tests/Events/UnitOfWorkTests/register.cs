using Cqrs.Framework.Aggregates;
using Cqrs.Framework.Events;
using Cqrs.Framework.Tests.Mocks;
using NUnit.Framework;
using Rhino.Mocks;

namespace Cqrs.Framework.Tests.Events.UnitOfWorkTests
{
    [TestFixture]
    public class register
    {
        [Test]
        public void should_save_registered_aggregate_upon_commit()
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
    }
}