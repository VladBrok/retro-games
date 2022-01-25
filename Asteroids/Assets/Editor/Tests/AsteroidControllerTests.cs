using System;
using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using Asteroids;

namespace Editor.Tests
{
    internal class AsteroidControllerTests
    {
        [Test]
        public void Constructor_BigAsteroidCountIsZero_Throws()
        {
            var spawner = Substitute.For<ISpawner<IDestructible>>();
            var origin = Substitute.For<ICenterProvider>();
            int bigAsteroidCount = 0;

            TestDelegate create = () => 
                new AsteroidController<IDestructible>(spawner, origin, bigAsteroidCount);

            Assert.Throws<ArgumentOutOfRangeException>(create);
        }

        [Test]
        public void OnAsteroidDestroyed_AllAsteroidsAreDestroyed_SpawnsNew()
        {
            var spawner = Substitute.For<ISpawner<IDestructible>>();
            var origin = Substitute.For<ICenterProvider>();
            int bigAsteroidCount = 1;
            var controller = new AsteroidController<IDestructible>(
                spawner, origin, bigAsteroidCount);

            RaiseDestroyedEvent(controller.AsteroidsLeft, spawner);

            spawner.Received().Spawn(bigAsteroidCount + 1, origin.Center);
        }

        private void RaiseDestroyedEvent(int count, ISpawner<IDestructible> spawner)
        {
            for (int i = 0; i < count; i++)
            {
                spawner.Destroyed +=
                    Raise.Event<Action<IDestructible>>(Substitute.For<IDestructible>());
            }
        }
    }
}