using System;
using UnityEngine;
using SnakeGame.Input;

namespace SnakeGame
{
    public class SnakeController
    {
        private readonly Snake _snake;
        private readonly IInputProvider _input;
        private Vector2 _movementDirection;

        public SnakeController(Snake snake, 
                               IInputProvider input, 
                               ITrigger food, 
                               Func<IBody> createBody)
        {
            _snake = snake;
            _input = input;
            food.TriggerEntered += () => _snake.AddBody(createBody()); ;

            _movementDirection = snake.MovementDirection;
        }

        public void UpdateMovement()
        {
            _snake.ChangeMovementDirection(_movementDirection);
            _snake.Move();
        }

        public void UpdateInput()
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
