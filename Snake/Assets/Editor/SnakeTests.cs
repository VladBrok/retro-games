using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using NSubstitute;
using SnakeGame;

namespace Editor
{
    public class SnakeTests
    {
        [Test]
        public void ChangeMovementDirection_DirectionIsZero_Throws()
        {
            var snake = new Snake(new IBody[Snake.MinBodyCount], Vector2.up);

            TestDelegate changeDirection = () =>
                snake.ChangeMovementDirection(Vector2.zero);

            Assert.Throws<ArgumentException>(changeDirection);
        }

        [Test]
        public void ChangeMovementDirection_XAndYAreBothNonZero_Throws(
            [Values(1f, -1f)] float x, [Values(1f, -1f)] float y)
        {
            var snake = new Snake(new IBody[Snake.MinBodyCount], Vector2.up);

            TestDelegate changeDirection = () =>
                snake.ChangeMovementDirection(new Vector2(x, y));

            Assert.Throws<ArgumentException>(changeDirection);
        }

        [Test]
        public void ChangeMovementDirection_CurrentIsHorizontalAndNewIsHorizontal_NotChanged(
            [Values(1f, -1f)] float xInit, [Values(1f, -1f)] float xNew)
        {
            var initDirection = new Vector2(xInit, 0f);
            var newDirection = new Vector2(xNew, 0f);
            var snake = new Snake(new IBody[Snake.MinBodyCount], initDirection);

            snake.ChangeMovementDirection(newDirection);

            Assert.AreEqual(initDirection, snake.MovementDirection);
        }

        [Test]
        public void ChangeMovementDirection_CurrentIsVerticalAndNewIsVertical_NotChanged(
            [Values(1f, -1f)] float yInit, [Values(1f, -1f)] float yNew)
        {
            var initDirection = new Vector2(0f, yInit);
            var newDirection = new Vector2(0f, yNew);
            var snake = new Snake(new IBody[Snake.MinBodyCount], initDirection);

            snake.ChangeMovementDirection(newDirection);

            Assert.AreEqual(initDirection, snake.MovementDirection);
        }

        [Test]
        public void ChangeMovementDirection_ValidHorizontalDirection_UsesSignOfDirection(
            [Values(2f, 0.5f, -2f, -0.5f)] float x)
        {
            var direction = new Vector2(x, 0f);
            var expected = new Vector2(Math.Sign(direction.x), Math.Sign(direction.y));
            var snake = new Snake(new IBody[Snake.MinBodyCount], Vector2.up);

            snake.ChangeMovementDirection(direction);

            Assert.AreEqual(expected, snake.MovementDirection);
        }

        [Test]
        public void ChangeMovementDirection_ValidVerticalDirection_UsesSignOfDirection(
            [Values(2f, 0.5f, -2f, -0.5f)] float y)
        {
            var direction = new Vector2(0f, y);
            var expected = new Vector2(Math.Sign(direction.x), Math.Sign(direction.y));
            var snake = new Snake(new IBody[Snake.MinBodyCount], Vector2.left);

            snake.ChangeMovementDirection(direction);

            Assert.AreEqual(expected, snake.MovementDirection);
        }
    }
}