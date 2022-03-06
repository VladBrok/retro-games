using System;
using UnityEngine;

namespace Arkanoid
{
    public class Brick : BrickBase
    {
        public static event Action Destroyed = delegate { };

        protected override void OnCollisionEnter2D(Collision2D _)
        {
            Destroy(gameObject);
            Destroyed.Invoke();
        }
    }
}