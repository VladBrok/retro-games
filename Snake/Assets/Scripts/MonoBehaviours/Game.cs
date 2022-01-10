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
        private SnakeController _snakeController;

        private void Start()
        {
            _snake = new Snake(_bodies.ToArray(), Vector2.up);
            _snakeController = new SnakeController(_snake);

            StartCoroutine(Move());
        }

        private void Update()
        {
            _snakeController.UpdateInput();
        }

        private IEnumerator Move()
        {
            for (; ; )
            {
                _snakeController.UpdateMovement();

                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}