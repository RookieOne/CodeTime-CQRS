using System.Linq;
using Cqrs.Framework.Tests.Mocks;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using NUnit.Framework;
using TestUtilities;

namespace Cqrs.Db4o.Tests
{
    [TestFixture]
    public class saving_new_aggregate
    {
        Db4oAggregate _aggregateInDb;
        Db4oEvent _eventInDb;

        [TestFixtureSetUp]
        public void SetUp()
        {
            MockAggregate aggregate;

            using (var store = new Db4oEventStore("db4oTest"))
            {
                aggregate = new MockAggregate();
                aggregate.Add(new MockEvent());

                store.Save(aggregate);
            }

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(Db4oEmbedded.NewConfiguration(), "db4oTest"))
            {
                _aggregateInDb = (from Db4oAggregate p in db
                                 where p.Id == aggregate.Id
                                 select p).FirstOrDefault();
                _eventInDb = (from Db4oEvent e in db
                              where e.AggregateId == aggregate.Id
                              select e).FirstOrDefault();
            }
        }

        [Test]
        public void should_insert_event_provider()
        {
            _aggregateInDb.ShouldNotBe(null);
        }

        [Test]
        public void should_set_version_number()
        {
            _aggregateInDb.Version.ShouldBe(1);
        }

        [Test]
        public void should_insert_event()
        {
           _eventInDb.ShouldNotBe(null); 
        }

        [Test]
        public void should_set_version_on_event()
        {
            _eventInDb.Version.ShouldBe(1);
        }
    }
}