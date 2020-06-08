using System;
using DataModel.GameResources;
using DataModel.GameData;
using NUnit.Framework;

namespace Tests.EditModeTests
{
    public class GameResourceSystemTest
    {
        private IResourceStorage _resourceStorage;
        private IClientResourceOperations _clientResourceOperations;
        private IServerResourceOperations _serverResourceOperations;
        
        private ResourceId Resource1 = new ResourceId("Resource1");
        private ResourceId Resource2 = new ResourceId("Resource2");

        [Test]
        public void TestSimpleResourceOperations()
        {
            InitNewResourceSystem();
            // Fail to request unexisted resource
            Assert.Throws<ArgumentException>(()=>
            {
                _resourceStorage.GetResourceAmount(Resource1);
            });
            // Simple set resource
            _serverResourceOperations.SetAmount(Resource1, 10);
            var r1Amount = _resourceStorage.GetResourceAmount(Resource1);
            Assert.AreEqual(r1Amount.Value, 10);

            // Override resource
            _serverResourceOperations.SetAmount(Resource1, 20);
            Assert.AreEqual(r1Amount.Value, 20);

            // Recieve resource
            _clientResourceOperations.Receive(Resource1, 5);
            Assert.AreEqual(r1Amount.Value, 25);

            // Spend resource
            var spendRes1 = _clientResourceOperations.TrySpend(Resource1, 15);
            Assert.AreEqual(r1Amount.Value, 10);
            Assert.IsTrue(spendRes1);
            var spendRes2 = _clientResourceOperations.TrySpend(Resource1, 15);
            Assert.AreEqual(r1Amount.Value, 10);
            Assert.IsFalse(spendRes2);
        }

        [Test]
        public void SetLimit()
        {
            InitNewResourceSystem();
            
            // No limits
            _serverResourceOperations.SetAmount(Resource1, 100000);
            _serverResourceOperations.SetAmount(Resource2, 100000);
            var r1Amount = _resourceStorage.GetResourceAmount(Resource1);
            var r2Amount = _resourceStorage.GetResourceAmount(Resource2);
            Assert.AreEqual(r1Amount.Value, 100000);
            Assert.AreEqual(r2Amount.Value, 100000);
            
            // Set limit
            _serverResourceOperations.SetLimit(Resource1, 100);
            _serverResourceOperations.SetLimit(Resource2, 200);
            Assert.AreEqual(r1Amount.Value, 100);
            Assert.AreEqual(r2Amount.Value, 200);
            
            // Add with limit
            _serverResourceOperations.SetAmount(Resource1, 99);
            _serverResourceOperations.SetAmount(Resource2, 99);
            Assert.AreEqual(r1Amount.Value, 99);
            Assert.AreEqual(r2Amount.Value, 99);
            
            _clientResourceOperations.Receive(Resource1, 11);
            _clientResourceOperations.Receive(Resource2, 11);
            Assert.AreEqual(r1Amount.Value, 100);
            Assert.AreEqual(r2Amount.Value, 110);
            
            // Remove limit
            _serverResourceOperations.SetLimit(Resource1, null);
            Assert.AreEqual(r1Amount.Value, 100);
            _clientResourceOperations.Receive(Resource1, 300);
            Assert.AreEqual(r1Amount.Value, 400);
        }

        private void InitNewResourceSystem()
        {
            var resourceSystem = new ClientResourcesStorage();
            _resourceStorage = resourceSystem;
            _clientResourceOperations = resourceSystem;
            _serverResourceOperations = resourceSystem;
        }
    }
}
