using UnityEngine;

namespace Arkanoid.Pickups.Effects
{
    public abstract class EffectBase : MonoBehaviour
    {
        [SerializeField] private Sprite _sprite;

        public Sprite Sprite { get { return _sprite; } }

        public abstract void Apply();
    }
}
