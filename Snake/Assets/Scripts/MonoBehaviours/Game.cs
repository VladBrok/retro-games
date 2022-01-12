﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SnakeGame.Input;
using UnityEngine.SceneManagement;
using System;

namespace SnakeGame.MonoBehaviours
{
    public class Game : MonoBehaviour
    {
        private static readonly int MainSceneIndex = 0;

        [SerializeField] private TriggerBody _headPrefab;
        [SerializeField] private TriggerBody _bodyPrefab;
        [SerializeField] private TriggerBody _foodPrefab;
        [SerializeField] private SpriteRenderer _gameFieldArea;
        [SerializeField] private UI _ui;

        private readonly WaitForSeconds _foodHideTime = new WaitForSeconds(0.1f);
        private readonly WaitForSeconds _snakeMoveDelay = new WaitForSeconds(0.15f);

        private SnakeController _snakeController;
        private Field _field;
        private Respawner _foodRespawner;

        private void Start()
        {
            Initialize();
            BeginGame();
        }

        private void Initialize()
        {
            TriggerBody food = Instantiate(_foodPrefab);
            TriggerBody snakeHead = Instantiate(_headPrefab);
            TriggerBody snakeBody = CreateSnakeBody();
            Snake snake = CreateSnake(snakeHead, snakeBody);

            _snakeController = CreateSnakeController(snake, food);
            _foodRespawner = CreateRespawner(food);
            _field = CreateField(snakeHead);

            _ui.Initialize(food);
        }

        private void BeginGame()
        {
            _foodRespawner.RespawnTarget();
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            for (; ; )
            {
                _snakeController.MoveSnake();
                yield return _snakeMoveDelay;
            }
        }

        private void Update()
        {
            _snakeController.Update();
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
            Action beforeRespawn = () => 
                StartCoroutine(HideTemporarly(food.gameObject, _foodHideTime));
            var respawner = new Respawner(food, _gameFieldArea.bounds, beforeRespawn);
            food.TriggerEntered += respawner.RespawnTarget;
            return respawner;
        }

        private TriggerBody CreateSnakeBody()
        {
            TriggerBody body = Instantiate(_bodyPrefab);
            body.TriggerEntered += GameOver;
            return body;
        }

        private IEnumerator HideTemporarly(GameObject toHide, WaitForSeconds hideTime)
        {
            toHide.SetActive(false);
            yield return hideTime;
            toHide.SetActive(true);
        }

        private void GameOver()
        {
            SceneManager.LoadScene(MainSceneIndex);
        }
    }
}