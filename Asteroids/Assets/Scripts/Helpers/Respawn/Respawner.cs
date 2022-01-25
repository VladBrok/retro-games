using System.Collections;
using UnityEngine;
using Asteroids.Extensions;

namespace Asteroids
{
    [RequireComponent(typeof(IHideable))]
    [DisallowMultipleComponent]
    public class Respawner : MonoBehaviour
    {
        [SerializeField] private Vector2 _position;
        [SerializeField] private float _delayInSeconds;
        [SerializeField] private LayerMask _mask;

        private Bounds _cameraView;
        private MonoBehaviour _coroutineStartHelper;
        private IHideable _target;
        private WaitForSeconds _waitForRespawnDelay;
        private RandomRespawner _randomRespawner;

        public void Respawn()
        {
            RespawnAt(_position);
        }

        private void Awake()
        {
            _target = GetComponent<IHideable>();
            _cameraView = Camera.main.GetViewBounds2D();
            _waitForRespawnDelay = new WaitForSeconds(_delayInSeconds);

            _coroutineStartHelper = new GameObject(
                gameObject.name + "CoroutineStartHelper").AddComponent<Empty>();
            _coroutineStartHelper.transform.parent = gameObject.transform.parent;

            // FIXME: Pass input and respawner as dependencies.
            _randomRespawner = new RandomRespawner(
                new KeyboardInput(), Random.Range, RespawnAt, _cameraView);
        }

        private void Update()
        {
            _randomRespawner.Update();
        }

        private void RespawnAt(Vector2 position)
        {
            _coroutineStartHelper.StartCoroutine(RespawnRoutine(position));
        }

        private IEnumerator RespawnRoutine(Vector2 position)
        {
            _target.Hide();
            do
            {
                yield return _waitForRespawnDelay;
            } while (!CanSpawnAt(position));
            _target.Center = position;
            _target.Show();
        }

        private bool CanSpawnAt(Vector2 position)
        {
            Vector2 boxSize = _target.Extents * 7;
            Collider2D other = Physics2D.OverlapBox(position, boxSize, 0f, _mask);
            return other == null;
        }
    }
}
