using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeGame.Input;
using UnityEngine.SceneManagement;

namespace SnakeGame.MonoBehaviours
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private TriggerBody _headPrefab;
        [SerializeField] private TriggerBody _bodyPrefab;
        [SerializeField] private TriggerBody _foodPrefab;
        [SerializeField] private GameField _gameField;
        [SerializeField] private SpriteRenderer _gameFieldArea;
        [SerializeField] private UI _ui;

        private readonly int _mainSceneIndex = 0;
        private SnakeController _snakeController;

        bool startWasCalled = false; // FIXME

        private void Start()
        {
            if (!startWasCalled)
            {
                startWasCalled = true;

                TriggerBody food = Instantiate(_foodPrefab);
                TriggerBody snakeHead = Instantiate(_headPrefab);

                var snake = new Snake(new TriggerBody[] { snakeHead, CreateBody() }, Vector2.up);
                var input = new KeyboardInput();
                _snakeController = new SnakeController(snake, input, food, CreateBody);

                var field = new Field(snakeHead, _gameFieldArea.bounds);
                field.TargetLeftField += GameOver;
                var respawner = new Respawner(food, _gameFieldArea.bounds);
                _gameField.Initialize(field, respawner, food);

                _ui.Initialize(food);
            }

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
            SceneManager.LoadScene(_mainSceneIndex);
        }
    }
}