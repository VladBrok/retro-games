using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeGame.Input;
using UnityEngine.SceneManagement;
using System;

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
        private Field _field;

        bool startWasCalled = false; // FIXME

        private void Start()
        {
            if (!startWasCalled)
            {
                startWasCalled = true;

                TriggerBody food = Instantiate(_foodPrefab);
                TriggerBody snakeHead = Instantiate(_headPrefab);
                TriggerBody snakeBody = CreateSnakeBody();

                Snake snake = CreateSnake(snakeHead, snakeBody);
                Field field = CreateField(snakeHead);
                Respawner respawner = CreateRespawner(food);

                _gameField.Initialize(field, respawner, food);
                _ui.Initialize(food);

                _snakeController = CreateSnakeController(snake, food);
            }

            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            for (; ; )
            {
                _snakeController.UpdateMovement();
                yield return new WaitForSeconds(0.15f);
            }
        }

        private void Update()
        {
            _snakeController.UpdateInput();
            _field.Update();
        }

        private Snake CreateSnake(IBody head, IBody body)
        {
            return new Snake(new IBody[] { head, body }, Vector2.up);
        }

        private SnakeController CreateSnakeController(Snake snake, ITrigger food)
        {
            var input = new KeyboardInput();
            return new SnakeController(snake, input, food, CreateSnakeBody);
        }

        private Field CreateField(IBody snakeHead)
        {
            var field = new Field(snakeHead, _gameFieldArea.bounds);
            field.TargetLeftField += GameOver;
            return field;
        }

        private Respawner CreateRespawner(TriggerBody food)
        {
            Action beforeRespawn = () => StartCoroutine(HideTemporarly(food.gameObject));
            var respawner = new Respawner(food, _gameFieldArea.bounds, beforeRespawn);
            food.TriggerEntered += respawner.RespawnTarget;
            respawner.RespawnTarget();
            return respawner;
        }

        private TriggerBody CreateSnakeBody()
        {
            TriggerBody body = Instantiate(_bodyPrefab);
            body.TriggerEntered += GameOver;
            return body;
        }

        private IEnumerator HideTemporarly(GameObject toHide)
        {
            toHide.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            toHide.SetActive(true);
        }

        private void GameOver()
        {
            SceneManager.LoadScene(_mainSceneIndex);
        }
    }
}