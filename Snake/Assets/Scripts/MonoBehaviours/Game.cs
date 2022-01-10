using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SnakeGame.MonoBehaviours
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private List<SnakeBody> _bodies;

        private Snake _snake;
        private Vector2 _movementDirection;

        private void Start()
        {
            _movementDirection = Vector2.up;

            _snake = new Snake(_movementDirection, _bodies.ToArray());

            StartCoroutine(Move());
        }

        private void Update()
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

        private IEnumerator Move()
        {
            for (; ; )
            {
                _snake.Move();

                yield return new WaitForSeconds(0.2f);

                _snake.ChangeMovementDirection(_movementDirection);
            }
        }
    }
}