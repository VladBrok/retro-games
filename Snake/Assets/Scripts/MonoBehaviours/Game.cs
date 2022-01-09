using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SnakeGame.MonoBehaviours
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private List<SnakeBody> Bodies;

        private Snake _snake;

        private void Start()
        {
            _snake = new Snake(Bodies.Select(
                    snakeBody => new Body(snakeBody, snakeBody.Renderer.size)).ToArray(),
                    Vector2.up);

            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            for (; ; )
            {
                _snake.Move();

                yield return new WaitForSeconds(0.2f);

                int randomIndex = Random.Range(0, Snake.AllowedDirections.Count);
                _snake.ChangeMovementDirection(Snake.AllowedDirections[randomIndex]);
            }
        }
    }
}