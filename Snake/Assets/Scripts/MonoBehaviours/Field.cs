using System.Collections;
using UnityEngine;

namespace SnakeGame.MonoBehaviours
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private Vector2 _size;
        [SerializeField] private Food _foodPrefab;

        private Bounds _bounds;
        private Food _food;
        private IBody _snakeHead;

        public Food Food { get { return _food; } }

        public void Initialize(IBody snakeHead)
        {
            _snakeHead = snakeHead;
        }

        private void Awake()
        {
            _bounds = new Bounds(transform.position, _size);
            _food = Instantiate(_foodPrefab, transform);
            _food.TriggerEntered += RespawnFood;

            RespawnFood();
        }

        private void Update()
        {
            bool snakeInsideField = _bounds.Contains(_snakeHead.Position);

            if (!snakeInsideField)
            {
                // FIXME
                Debug.Log("<color=red>Game over.</color>");
                Time.timeScale = 0f;
                gameObject.SetActive(false);
            }
        }

        private void RespawnFood()
        {
            StartCoroutine(HideFoodTemporarly());

            var randomPosition = new Vector2(
                Mathf.Floor(Random.Range(_bounds.min.x, _bounds.max.x - _food.Size.x)),
                Mathf.Floor(Random.Range(_bounds.min.y + _food.Size.y, _bounds.max.y)));

            _food.Position = randomPosition;
        }

        private IEnumerator HideFoodTemporarly()
        {
            _food.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            _food.gameObject.SetActive(true);
        }
    }
}
