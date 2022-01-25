using UnityEngine;

namespace Asteroids
{
    public interface IWrapable : ICenterProvider
    {
        Vector2 Extents { get; }
        bool Intersects(Bounds other);
    }
}
