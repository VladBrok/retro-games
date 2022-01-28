using System;
using UnityEngine;
using SnakeGame.Input;
using SnakeGame.Snakes;

namespace SnakeGame.Controllers
{
    public class SnakeController
    {
        private readonly ISnake _snake;
        private readonly IMovementInput _input;
        private Vector2 _movementDirection;

        public SnakeController(ISnake snake,
                               IMovementInput input,
                               ITrigger food,
                               Func<IBody> createBody)
        {
            _snake = snake;
            _input = input;
            food.TriggerEntered += () => _snake.AddBody(createBody());

            _movementDirection = snake.MovementDirection;
        }

        public void MoveSnake()
        {
            _snake.ChangeMovementDirection(_movementDirection);
            _snake.Move();
        }

        public void Update()
        {
            float horizontal = _input.Horizontal;
            float vertical = _input.Vertical;
            if (vertical != 0f)
            {
                horizontal = 0f;
            }

            if (horizontal != 0f || vertical != 0f)
            {
                _movementDirection = new Vector2(horizontal, vertical);
            }
        }
    }
}