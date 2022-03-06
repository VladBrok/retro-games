using System;
using UnityEngine;

namespace Arkanoid
{
    [RequireComponent(typeof(Collider2D))]
    public class Floor : MonoBehaviour
    {
        [SerializeField] private Ball _ball;
        [SerializeField] private Paddle _paddle;

        public event Action BallDead = delegate { };

        private void Awake()
        {
            Debug.Assert(
                GetComponent<Collider2D>().isTrigger,
                gameObject.name + " should have a trigger collider.");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.GetComponent<Ball>()) return;

            _paddle.Reset();
            _ball.Reset();
            BallDead.Invoke();
        }
    }
}