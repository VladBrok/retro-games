using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using SnakeGame.Input;
using SnakeGame.Snakes;
using SnakeGame.Helpers;
using SnakeGame.Controllers;

namespace SnakeGame.MonoBehaviours
{
    [RequireComponent(typeof(Pauser))]
    public class Game : MonoBehaviour
    {
        private static readonly int MainSceneIndex = 0;

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private TriggerBody _headPrefab;
        [SerializeField] private TriggerBody _bodyPrefab;
        [SerializeField] private TriggerBody _foodPrefab;
        [SerializeField] private SpriteRenderer _gameFieldArea;
        [SerializeField] private UI _ui;

        private readonly WaitForSeconds _waitForHideDelay = new WaitForSeconds(0.1f);
        private readonly WaitForSeconds _waitForMoveDelay = new WaitForSeconds(0.15f);
        private readonly Vector2 _initialMovementDirection = Vector2.up;

        private SnakeController _snakeController;
        private FieldPositionsController _positionsController;
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
            TriggerBody snakeHead = CreateSnakeHead();
            TriggerBody snakeBody = CreateSnakeBody();
            ISnake snake = CreateSnake(snakeHead, snakeBody);
            KeyboardInput input = CreateInput();

            _positionsController = CreatePositionsController(snake);
            _snakeController = CreateSnakeController(input, snake, food);
            _foodRespawner = CreateRespawner(snake, food);
            _field = CreateField(snakeHead);

            _ui.Initialize(food);
            _ui.QuitButtonClicked.AddListener(Quit);

            GetComponent<Pauser>().Initialize(input);
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
                yield return _waitForMoveDelay;
            }
        }

        private void Update()
        {
            _snakeController.Update();
            _positionsController.Update();
            _field.Update();
        }

        private TriggerBody CreateSnakeHead()
        {
            TriggerBody head = Instantiate(_headPrefab);
            head.Position = _spawnPoint.position + new Vector3(
                head.Size.x * _initialMovementDirection.x,
                head.Size.y * _initialMovementDirection.y);
            return head;
        }

        private TriggerBody CreateSnakeBody()
        {
            TriggerBody body = Instantiate(
                _bodyPrefab, _spawnPoint.position, Quaternion.identity);
            body.TriggerEntered += GameOver;
            return body;
        }

        private Snake CreateSnake(IBody head, IBody body)
        {
            return new Snake(new IBody[] { head, body }, _initialMovementDirection);
        }

        private FieldPositionsController CreatePositionsController(ISnake snake)
        {
            var controller = new FieldPositionsController(snake, _gameFieldArea.bounds);
            controller.AllPositionsOccupied += Victory;
            return controller;
        }

        private KeyboardInput CreateInput()
        {
            return new KeyboardInput();
        }

        private SnakeController CreateSnakeController(IMovementInput input, 
                                                      ISnake snake, 
                                                      ITrigger food)
        {
            return new SnakeController(snake, input, food, CreateSnakeBody);
        }

        private Field CreateField(IBody snakeHead)
        {
            var field = new Field(snakeHead, _gameFieldArea.bounds);
            field.TargetLeftField += GameOver;
            return field;
        }

        private Respawner CreateRespawner(ISnake snake, TriggerBody food)
        {
            Action beforeRespawn = () => 
                StartCoroutine(HideTemporarly(food.gameObject));
            var respawner = new Respawner(food, _positionsController, beforeRespawn);
            food.TriggerEntered += respawner.RespawnTarget;
            return respawner;
        }

        private IEnumerator HideTemporarly(GameObject toHide)
        {
            toHide.SetActive(false);
            yield return _waitForHideDelay;
            toHide.SetActive(true);
        }

        private void GameOver()
        {
            Debug.Log("<color=red>Game over.</color>");
        }

        private void Victory()
        {
            Debug.Log("<color=green>VICTORY!</color>");
            Time.timeScale = 0f;
        }

        private void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}