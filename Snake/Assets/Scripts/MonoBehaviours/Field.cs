using System.Collections;
using UnityEngine;

namespace SnakeGame.MonoBehaviours
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _background;
        [SerializeField] private TriggerBody _foodPrefab;

        private Bounds _bounds;
        private TriggerBody _food;
        private IBody _snakeHead;

        public TriggerBody Food { get { return _food; } }

        public void Initialize(IBody snakeHead)
        {
            _snakeHead = snakeHead;
        }

        private void Awake()
        {
            _bounds = _background.bounds;
            _food = Instantiate(_foodPrefab, transform);
            _food.TriggerEntered += RespawnFood;

            RespawnFood();
        }

        private void Update()
        {
            if (!IsSnakeOnField())
            {
                // FIXME
                Debug.Log("<color=red>Game over.</color>");
            }
        }

        private bool IsSnakeOnField()
        {
            float offset = 0.5f;
            var snakePos = new Vector2(
                _snakeHead.Position.x + offset, 
                _snakeHead.Position.y - offset);
            return _bounds.Contains(snakePos);
        }

        private void RespawnFood()
        {
            StartCoroutine(HideTemporarly(_food.gameObject));
            _food.Position = GetRandomPosition();
        }

        private IEnumerator HideTemporarly(GameObject toHide)
        {
            toHide.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            toHide.SetActive(true);
        }

        private Vector2 GetRandomPosition()
        {
            float offset = 1f;
            return new Vector2(
                Mathf.Floor(Random.Range(_bounds.min.x, _bounds.max.x - offset)),
                Mathf.Floor(Random.Range(_bounds.min.y + offset, _bounds.max.y)));  
        }
    }
}
