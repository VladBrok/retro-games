using System;
using System.Collections.Generic;
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
            var pools = new Dictionary<AsteroidType, IPool<IAsteroid>>();
            var origin = Substitute.For<ICenterProvider>();
            int bigAsteroidCount = 0;

            TestDelegate create = () =>
                new AsteroidController<IAsteroid>(pools, origin, bigAsteroidCount);

            Assert.Throws<ArgumentOutOfRangeException>(create);
        }

        [Test]
        public void OnObjectDestroyed_AllAsteroidsAreDestroyed_SpawnsNew()
        {
            var pools = new Dictionary<AsteroidType, IPool<IAsteroid>>();
            pools.Add(AsteroidType.Big, Substitute.For<IPool<IAsteroid>>());
            pools.Add(AsteroidType.Medium, Substitute.For<IPool<IAsteroid>>());
            var origin = Substitute.For<ICenterProvider>();
            int bigAsteroidCount = 1;
            var controller = new AsteroidController<IAsteroid>(
                pools, origin, bigAsteroidCount);

            RaiseObjectDestroyedEvent(controller.AsteroidsLeft, pools[AsteroidType.Big]);

            pools[AsteroidType.Big].Received().Get(bigAsteroidCount + 1, origin.Center);
        }

        private void RaiseObjectDestroyedEvent(int count, IPool<IAsteroid> pool)
        {
            for (int i = 0; i < count; i++)
            {
                pool.ObjectDestroyed +=
                    Raise.Event<Action<IAsteroid>>(Substitute.For<IAsteroid>());
            }
        }
    }

    public interface IAsteroid : IDestructible, ICenterProvider { }
}