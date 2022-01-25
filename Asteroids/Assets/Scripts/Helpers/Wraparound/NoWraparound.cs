using UnityEngine;

namespace Asteroids
{
    class NoWraparound<T> : WraparoundBase<T> where T : Destructible<T>
    {
        public NoWraparound(T self, Bounds viewArea)
            : base(self, viewArea)
        {
        }

        protected override void ApplyWraparound()
        {
            Self.Destroy();
        }
    }
}
