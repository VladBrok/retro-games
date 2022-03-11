using UnityEngine;

namespace Arkanoid.Pickups.Effects
{
    public class LifeEffect : EffectBase
    {
       [SerializeField] private MainUI _ui;

        public override void Apply()
        {
            _ui.AddOneLife();
        }
    }
}
