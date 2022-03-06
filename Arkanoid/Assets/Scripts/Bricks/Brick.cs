using System;
using UnityEngine;

namespace Arkanoid
{
    public class Brick : BrickBase
    {
        public static event Action<BrickDestroyedData> Destroyed = delegate { };

        protected override void OnCollisionEnter2D(Collision2D _)
        {
            Destroy(gameObject);
            Destroyed.Invoke(new BrickDestroyedData { Position = transform.position });
        }
    }

    public class BrickDestroyedData
    {
        public Vector2 Position { get; set; }
    }
}