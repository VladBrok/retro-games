using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeGame.Input;

namespace SnakeGame.MonoBehaviours
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private TriggerBody _headPrefab;
        [SerializeField] private TriggerBody _bodyPrefab;
        [SerializeField] private Field _field;
        [SerializeField] private UI _ui;

        private SnakeController _snakeController;

        private void Start()
        {
            TriggerBody snakeHead = Instantiate(_headPrefab);
            var snake = new Snake(new TriggerBody[] { snakeHead, CreateBody() }, Vector2.up);
            var input = new KeyboardInput();
            _snakeController = new SnakeController(snake, input, _field.Food, CreateBody);

            _field.Initialize(snakeHead);
            _field.SnakeLeftField += GameOver;

            _ui.Initialize(_field.Food);

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
                yield return new WaitForSeconds(0.15f);
            }
        }

        private TriggerBody CreateBody()
        {
            TriggerBody body = Instantiate(_bodyPrefab);
            body.TriggerEntered += GameOver;
            return body;
        }

        private void GameOver()
        {
            _ui.GameOver();
        }
    }
}