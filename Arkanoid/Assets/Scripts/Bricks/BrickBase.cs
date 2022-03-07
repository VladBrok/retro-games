using UnityEngine;

namespace Arkanoid
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class BrickBase : MonoBehaviour
    {
        protected abstract void HandleCollision();
    }
}