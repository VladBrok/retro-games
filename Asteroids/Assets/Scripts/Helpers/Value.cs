using System;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public sealed class Value
    {
        [SerializeField] private float _min;
        [SerializeField] private float _max;

        public Value(float min, float max)
        {
            _min = min;
            _max = max;
        }

        public float Min
        {
            get { return _min; }
        }
        public float Max
        {
            get { return _max; }
        }
    }
}
