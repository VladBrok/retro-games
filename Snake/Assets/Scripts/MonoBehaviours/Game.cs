using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.MonoBehaviours
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private SnakeBody _headPrefab;
        [SerializeField] private SnakeBody _bodyPrefab;
        [SerializeField] private Field _field;

        private SnakeController<SnakeBody> _snakeController;

        private void Start()
        {
            var snake = new Snake<SnakeBody>(new SnakeBody[] 
            { 
                Instantiate(_headPrefab),
                Instantiate(_bodyPrefab)
            });
            var input = new KeyboardInput();
            _snakeController = new SnakeController<SnakeBody>(snake, input);

            snake.BodyAdded += body => body.gameObject.SetActive(true);

            _field.Food.TriggerEntered += () => 
            {
                var body = Instantiate(_bodyPrefab);
                body.gameObject.SetActive(false);
                snake.RequestBodyAddition(body);
            };

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