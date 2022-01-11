using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeGame.Input;

namespace SnakeGame.MonoBehaviours
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private SpriteBody _headPrefab;
        [SerializeField] private SpriteBody _bodyPrefab;
        [SerializeField] private Field _field;

        private SnakeController _snakeController;

        private void Start()
        {
            SpriteBody snakeHead = Instantiate(_headPrefab);
            var snake = new Snake(new SpriteBody[] 
            { 
                snakeHead,
                Instantiate(_bodyPrefab)
            }, Vector2.up);
            var input = new KeyboardInput();
            _snakeController = new SnakeController(snake, input);

            _field.Initialize(snakeHead);
            _field.Food.TriggerEntered += () => { snake.AddBody(Instantiate(_bodyPrefab)); };

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