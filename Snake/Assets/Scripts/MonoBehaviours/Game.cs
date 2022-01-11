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

        private SnakeController _snakeController;

        private void Start()
        {
            TriggerBody snakeHead = Instantiate(_headPrefab);
            var snake = new Snake(new TriggerBody[] { snakeHead, CreateBody() }, Vector2.up);
            var input = new KeyboardInput();
            _snakeController = new SnakeController(snake, input);

            _field.Initialize(snakeHead);
            _field.Food.TriggerEntered += () => { snake.AddBody(CreateBody()); };

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
            // FIXME
            body.TriggerEntered += () => Debug.Log("<color=red>Collided with head</color>");
            return body;
        }
    }
}