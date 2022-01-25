using UnityEngine;

namespace Asteroids
{
    public abstract class WraparoundBase<T> where T : IWrapable
    {
        private readonly T _self;
        private readonly Bounds _viewArea;

        public WraparoundBase(T self, Bounds viewArea)
        {
            _self = self;
            _viewArea = viewArea;
        }

        protected T Self
        {
            get { return _self; }
        }
        protected Bounds ViewArea
        {
            get { return _viewArea; }
        }

        public void Update()
        {
            bool outsideOfView = !_self.Intersects(_viewArea);
            if (outsideOfView)
            {
                ApplyWraparound();
            }
        }

        protected abstract void ApplyWraparound();
    }
}
