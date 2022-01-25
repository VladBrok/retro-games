using UnityEngine;

namespace Asteroids
{
    public abstract class ConsistentlyMovingObject<T> : Destructible<T>
        where T : IWrapable
    {
        private IMovement _movement;

        public IMovement Movement
        {
            get { return _movement; }
        }

        public void Initialize(WraparoundBase<T> wraparound, IMovement movement)
        {
            base.Initialize(wraparound);
            _movement = movement;
        }

        protected override void Update()
        {
            base.Update();
            _movement.Move(Time.deltaTime);
        }
    }
}
