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
                "Collider of the " + gameObject.name + " should be a trigger.");
        }

        private void OnTriggerEnter2D(Collider2D _)
        {
            _paddle.Reset();
            _ball.Reset();
            BallDead.Invoke();
        }
    }
}