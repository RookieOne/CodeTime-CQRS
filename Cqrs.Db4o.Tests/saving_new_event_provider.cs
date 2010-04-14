using System.Linq;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using NUnit.Framework;
using TestUtilities;

namespace Cqrs.Db4o.Tests
{
    [TestFixture]
    public class saving_new_event_provider
    {
        Db4oEventProvider _providerInDb;
        Db4oEvent _eventInDb;

        [TestFixtureSetUp]
        public void SetUp()
        {
            MockEventProvider provider;

            using (var store = new Db4oEventStore("db4oTest"))
            {
                provider = new MockEventProvider();
                provider.Add(new MockEvent());

                store.Save(provider);
            }

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(Db4oEmbedded.NewConfiguration(), "db4oTest"))
            {
                _providerInDb = (from Db4oEventProvider p in db
                                 where p.Id == provider.Id
                                 select p).FirstOrDefault();
                _eventInDb = (from Db4oEvent e in db
                              where e.EventProviderId == provider.Id
                              select e).FirstOrDefault();
            }
        }

        [Test]
        public void should_insert_event_provider()
        {
            _providerInDb.ShouldNotBe(null);
        }

        [Test]
        public void should_set_version_number()
        {
            _providerInDb.Version.ShouldBe(1);
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