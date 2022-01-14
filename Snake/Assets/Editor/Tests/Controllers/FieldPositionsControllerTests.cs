using System.Linq;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using SnakeGame.Snakes;
using SnakeGame.Controllers;

namespace SnakeGame.Editor.Tests.Controllers
{
    public class FieldPositionsControllerTests
    {
        [Test]
        public void Update_AllPositionsAreOccupied_RaisesEvent()
        {
            Bounds fieldArea = new Bounds(Vector2.zero, Vector2.one);
            ISnake snake = CreateDefaultSnake(fieldArea);
            var controller = new FieldPositionsController(snake, fieldArea);
            bool eventRaised = false;
            controller.AllPositionsOccupied += () => eventRaised = true;

            controller.Update();

            Assert.That(eventRaised);
        }

        [Test]
        public void Update_SnakeMoved_PreviousSnakeHeadPositionIsEmpty()
        {
            Bounds fieldArea = CreateDefaultFieldArea();
            ISnake snake = CreateDefaultSnake(fieldArea);
            var controller = new FieldPositionsController(snake, fieldArea);
            Vector2 previousHeadPosition = snake.Head.Position;

            MoveRight(snake);
            controller.Update();

            Assert.That(controller.EmptyPositions.Contains(previousHeadPosition));
        }

        [Test]
        public void Update_SnakeMoved_CurrentSnakeHeadPositionIsNotEmpty()
        {
            Bounds fieldArea = CreateDefaultFieldArea();
            ISnake snake = CreateDefaultSnake(fieldArea);
            var controller = new FieldPositionsController(snake, fieldArea);

            MoveRight(snake);
            controller.Update();

            Assert.IsFalse(controller.EmptyPositions.Contains(snake.Head.Position));
        }

        private Bounds CreateDefaultFieldArea()
        {
            return new Bounds(Vector2.zero, new Vector2(2f, 2f));
        }

        private ISnake CreateDefaultSnake(Bounds fieldArea)
        {
            var defaultPosition = new Vector2(fieldArea.min.x, fieldArea.max.y);
            var snake = Substitute.For<ISnake>();
            snake.Head.Position.Returns(defaultPosition);
            snake.Tip.Position.Returns(defaultPosition);
            return snake;
        }

        private void MoveRight(ISnake snake)
        {
            snake.Head.Position.Returns(snake.Head.Position + Vector2.right);
            snake.Tip.Position.Returns(snake.Tip.Position + Vector2.right);
        }
    }
}
