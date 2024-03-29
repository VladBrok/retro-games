﻿using UnityEngine;
using NSubstitute;
using NUnit.Framework;
using SnakeGame.Controllers;
using SnakeGame.Helpers;

namespace SnakeGame.Editor.Tests.Helpers
{
    public class RespawnerTests
    {
        [Test]
        public void RespawnTarget_EmptyPositionsAreAvailable_CallsProvidedDelegate()
        {
            var target = Substitute.For<IBody>();
            var positionProvider = Substitute.For<IEmptyPositionsProvider>();
            bool delegateCalled = false;
            var respawner = new Respawner(target, positionProvider, () => delegateCalled = true);
            positionProvider.EmptyPositions.Returns(new[] { Vector2.zero });

            respawner.RespawnTarget();

            Assert.IsTrue(delegateCalled);
        }

        [Test]
        public void RespawnTarget_NoEmptyPositions_DelegateIsNotCalled()
        {
            var target = Substitute.For<IBody>();
            var positionProvider = Substitute.For<IEmptyPositionsProvider>();
            bool delegateCalled = false;
            var respawner = new Respawner(target, positionProvider, () => delegateCalled = true);

            respawner.RespawnTarget();

            Assert.IsFalse(delegateCalled);
        }

        [Test]
        public void RespawnTarget_OneEmptyPosition_RespawnsAtThatPosition()
        {
            var target = Substitute.For<IBody>();
            var positionProvider = Substitute.For<IEmptyPositionsProvider>();
            var respawner = new Respawner(target, positionProvider, delegate { });
            var emptyPosition = new Vector2(1f, 1f);
            positionProvider.EmptyPositions.Returns(new Vector2[] { emptyPosition });

            respawner.RespawnTarget();

            Assert.AreEqual(emptyPosition, target.Position);
        }
    }
}
