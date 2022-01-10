using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeGame.Input;

namespace SnakeGame.MonoBehaviours
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private SnakeBody _headPrefab;
        [SerializeField] private SnakeBody _bodyPrefab;
        [SerializeField] private Field _field;

        private SnakeController _snakeController;

        private void Start()
        {
            var snake = new Snake(new SnakeBody[] 
            { 
                Instantiate(_headPrefab),
                Instantiate(_bodyPrefab)
            });
            var input = new KeyboardInput();
            _snakeController = new SnakeController(snake, input);

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