using System;
using UnityEngine;

namespace Asteroids
{
    public class RandomRespawner
    {
        private readonly IRespawnInput _input;
        private readonly Func<float, float, float> _randomRange;
        private readonly Action<Vector2> _respawnAt;
        private readonly Bounds _viewArea;

        public RandomRespawner(
            IRespawnInput input, 
            Func<float, float, float> randomRange,
            Action<Vector2> respawnAt,
            Bounds viewArea)
        {
            _input = input;
            _randomRange = randomRange;
            _respawnAt = respawnAt;
            _viewArea = viewArea;
        }

        public void Update()
        {
            if (_input.Respawn)
            {
                var respawnLocation = new Vector2(
                    _randomRange(_viewArea.min.x, _viewArea.max.x),
                    _randomRange(_viewArea.min.y, _viewArea.max.y));
                _respawnAt(respawnLocation);
            }
        }
    }
}
