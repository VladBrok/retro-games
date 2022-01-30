using System.Collections;
using UnityEngine;
using Asteroids.Extensions;
using System;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [DisallowMultipleComponent]
    public class Respawner : MonoBehaviour
    {
        [SerializeField] private float _delayInSeconds;
        [SerializeField] private LayerMask _mask;

        private IActivable _target;
        private IRespawnInput _input;
        private Bounds _viewArea;
        private WaitForSeconds _waitForRespawnDelay;

        public void Initialize(IActivable target, IRespawnInput input, Bounds viewArea)
        {
            _target = target;
            _input = input;
            _viewArea = viewArea;
            _waitForRespawnDelay = new WaitForSeconds(_delayInSeconds);
        }

        public void RespawnAt(Vector2 position)
        {
            StartCoroutine(RespawnRoutine(position));
        }

        private void Update()
        {
            if (_input.Respawn)
            {
                var respawnLocation = new Vector2(
                    Random.Range(_viewArea.min.x, _viewArea.max.x),
                    Random.Range(_viewArea.min.y, _viewArea.max.y));
                RespawnAt(respawnLocation);
            }
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
