using System;
using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using Asteroids;

namespace Editor.Tests
{
    internal class LifeControllerTests
    {
        [Test]
        public void Constructor_MaxLivesIsZero_Throws()
        {
            var target = Substitute.For<IDestructible>();
            var maxLives = 0;

            TestDelegate create = () => new LifeController(target, maxLives);

            Assert.Throws<ArgumentOutOfRangeException>(create);
        }

        [Test]
        public void TargetDestroyed_OnLifeLeft_RaisesLostLifeEvent()
        {
            var target = Substitute.For<IDestructible>();
            var maxLives = 2;
            var lifeController = new LifeController(target, maxLives);
            bool wasRaised = false;
            lifeController.LostLife += () => wasRaised = true;

            target.Destroyed += Raise.Event<Action>();

            Assert.IsTrue(wasRaised);
        }

        [Test]
        public void TargetDestroyed_NoLivesLeft_RaisesDeadEvent()
        {
            var target = Substitute.For<IDestructible>();
            var maxLives = 1;
            var lifeController = new LifeController(target, maxLives);
            bool wasRaised = false;
            lifeController.Dead += () => wasRaised = true;

            target.Destroyed += Raise.Event<Action>();

            Assert.IsTrue(wasRaised);
        }
    }
}
