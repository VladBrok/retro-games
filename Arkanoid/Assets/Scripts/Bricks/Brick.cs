using System;
using UnityEngine;

namespace Arkanoid
{
    public class Brick : BrickBase
    {
        public static event Action<BrickDestroyedData> Destroyed = delegate { };

        protected override void HandleCollision()
        {
            Destroy(gameObject);
            Destroyed.Invoke(new BrickDestroyedData { Position = transform.position });
        }

        private void OnCollisionEnter2D(Collision2D _)
        {
            HandleCollision();
        }

        private void OnTriggerEnter2D(Collider2D _)
        {
            HandleCollision();
        }
    }

    public class BrickDestroyedData
    {
        public Vector2 Position { get; set; }
    }
}