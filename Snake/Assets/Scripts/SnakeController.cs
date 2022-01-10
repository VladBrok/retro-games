using System;
using UnityEngine;

namespace SnakeGame
{
    public class SnakeController
    {
        private readonly Snake _snake;
        private Vector2 _movementDirection;

        public SnakeController(Snake snake)
        {
            _snake = snake;
        }

        public void UpdateMovement()
        {
            _snake.ChangeMovementDirection(_movementDirection);
            _snake.Move();
        }

        public void UpdateInput()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
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

