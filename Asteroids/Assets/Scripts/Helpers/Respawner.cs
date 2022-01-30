using System.Collections;
using UnityEngine;
using Asteroids.Extensions;
using System;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [RequireComponent(typeof(IActivable))]
    [DisallowMultipleComponent]
    public class Respawner : MonoBehaviour
    {
        [SerializeField] private Vector2 _position;
        [SerializeField] private float _delayInSeconds;
        [SerializeField] private LayerMask _mask;

        private Bounds _cameraView;
        private MonoBehaviour _coroutineStartHelper;
        private IActivable _target;
        private WaitForSeconds _waitForRespawnDelay;
        private IRespawnInput _input;

        public void Respawn()
        {
            RespawnAt(_position);
        }

        private void Awake()
        {
            _target = GetComponent<IActivable>();
            _cameraView = Camera.main.GetViewBounds2D();
            _waitForRespawnDelay = new WaitForSeconds(_delayInSeconds);

            _coroutineStartHelper = new GameObject(
                gameObject.name + "CoroutineStartHelper").AddComponent<Empty>();
            _coroutineStartHelper.transform.parent = gameObject.transform.parent;

            // FIXME: Pass input a dependenciy
            _input = new KeyboardInput();
        }

        private void Update()
        {
            if (_input.Respawn)
            {
                var respawnLocation = new Vector2(
                    Random.Range(_cameraView.min.x, _cameraView.max.x),
                    Random.Range(_cameraView.min.y, _cameraView.max.y));
                RespawnAt(respawnLocation);
            }
        }

        private void RespawnAt(Vector2 position)
        {
            _coroutineStartHelper.StartCoroutine(RespawnRoutine(position));
        }

        private IEnumerator RespawnRoutine(Vector2 position)
        {
            _target.Activate();
            do
            {
                yield return _waitForRespawnDelay;
            } while (!CanSpawnAt(position));
            _target.Center = position;
            _target.Deactivate();
        }

        private bool CanSpawnAt(Vector2 position)
        {
            Vector2 boxSize = _target.Extents * 7;
            Collider2D other = Physics2D.OverlapBox(position, boxSize, 0f, _mask);
            return other == null;
        }
    }
}
