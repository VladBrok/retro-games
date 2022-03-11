using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Arkanoid.Pickups;
using Arkanoid.Pickups.Effects;

namespace Arkanoid.Controllers
{
    public class GamePauser : MonoBehaviour
    {
        [SerializeField] private Paddle _paddle;
        [SerializeField] private Ball _ball;
        [SerializeField] private PickupController _pickupController;
        [SerializeField] private EffectBase[] _effects;

        private List<IPausable> _pausables;

        public void Pause()
        {
            _pausables.ForEach(p => p.Pause());
        }

        public void Unpause()
        {
            _pausables.ForEach(p => p.Unpause());
        }

        private void Awake()
        {
            _pausables = new List<IPausable>();
            _pausables.Add(_ball);
            _pausables.Add(_paddle);
            _pausables.AddRange(_effects.OfType<IPausable>());
            _pickupController.PickupSpawned += p => _pausables.Add(p);
            _pickupController.PickupDestroyed += p => _pausables.Remove(p);
        }
    }
}
