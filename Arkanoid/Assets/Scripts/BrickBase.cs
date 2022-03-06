using UnityEngine;

namespace Arkanoid
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class BrickBase : MonoBehaviour
    {
        protected virtual void Awake()
        {
            Debug.Assert(
                !GetComponent<Collider2D>().isTrigger, 
                gameObject.name + " should have a non-trigger collider.");
        }

        protected abstract void OnCollisionEnter2D(Collision2D other);
    }
}