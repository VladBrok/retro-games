using UnityEngine;

namespace Arkanoid.Pickups
{
    public class LifePickup : PickupBase
    {
        private UI _ui;

        public override void Initialize(PickupConfig config)
        {
            base.Initialize(config);
            _ui = config.UI;
        }

        protected override void ApplyEffect()
        {
            _ui.AddOneLife();
        }
    }
}
