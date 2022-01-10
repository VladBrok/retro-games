using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.MonoBehaviours
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private List<SnakeBody> _bodies;

        private SnakeController _snakeController;

        private void Start()
        {
            var snake = new Snake(_bodies.ToArray());
            var input = new KeyboardInput();
            _snakeController = new SnakeController(snake, input);

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