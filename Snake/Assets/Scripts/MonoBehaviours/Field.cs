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

        public Food Food { get { return _food; } }

        private void Awake()
        {
            _bounds = new Bounds(transform.position, _size);
            _food = Instantiate(_foodPrefab, transform);
            RespawnFood();
            _food.TriggerEntered += RespawnFood;
        }

        private void RespawnFood()
        {
            StartCoroutine(HideFoodTemporarly());

            var randomPosition = new Vector2(
                Mathf.Floor(Random.Range(_bounds.min.x, _bounds.max.x)),
                Mathf.Floor(Random.Range(_bounds.min.y, _bounds.max.y)));

            _food.transform.position = randomPosition;
        }

        private IEnumerator HideFoodTemporarly()
        {
            _food.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.1f);

            _food.gameObject.SetActive(true);
        }
    }
}
