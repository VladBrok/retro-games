using System;
using UnityEngine;

namespace SnakeGame
{
    public class SnakeController
    {
        private readonly Snake _snake;
        private readonly IInputProvider _input;
        private Vector2 _movementDirection;

        public SnakeController(Snake snake, IInputProvider input)
        {
            _snake = snake;
            _input = input;
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
            
            _movementDirection = new Vector2(horizontal, vertical);
        }
    }
}

