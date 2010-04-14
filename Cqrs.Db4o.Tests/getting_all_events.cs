using System.Collections.Generic;
using System.Linq;
using Cqrs.Framework.Events;
using Cqrs.Framework.Tests.Mocks;
using NUnit.Framework;
using TestUtilities;

namespace Cqrs.Db4o.Tests
{
    [TestFixture]
    public class getting_all_events
    {
        IEnumerable<IDomainEvent> _events;
        MockEvent _firstEvent;
        MockEvent _secondEvent;

        [TestFixtureSetUp]
        public void SetUp()
        {
            MockAggregate provider;
            _firstEvent = new MockEvent();
            _secondEvent = new MockEvent();

            using (var store = new Db4oEventStore("db4oTest"))
            {
                provider = new MockAggregate();
                provider.Add(_firstEvent);
                provider.Add(_secondEvent);

                store.Save(provider);
            }

            using (var store = new Db4oEventStore("db4oTest"))
            {
                _events = store.GetAllEvents(provider.Id);
            }
        }

        [Test]
        public void should_order_by_version()
        {
            _events.ElementAt(0).Id.ShouldBe(_firstEvent.Id);
            _events.ElementAt(1).Id.ShouldBe(_secondEvent.Id);
        }

        [Test]
        public void should_return_all_events()
        {
            _events.Count().ShouldBe(2);
        }
    }
}