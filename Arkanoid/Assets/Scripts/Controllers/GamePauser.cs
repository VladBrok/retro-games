using System.Collections.Generic;
using UnityEngine;
using Arkanoid.Pickups;

namespace Arkanoid.Controllers
{
    public class GamePauser : MonoBehaviour
    {
        [SerializeField] private Paddle _paddle;
        [SerializeField] private Ball _ball;
        [SerializeField] private PickupController _pickupController;

        private List<PickupBase> _pickups;

        public void Pause()
        {
            _paddle.Pause();
            _ball.Pause();
            _pickups.ForEach(p => p.Pause());
        }

        public void Unpause()
        {
            _paddle.Unpause();
            _ball.Unpause();
            _pickups.ForEach(p => p.Unpause());
        }

        private void Awake()
        {
            _pickups = new List<PickupBase>();
            _pickupController.PickupCreated += OnPickupCreated;
            _pickupController.PickupDestroyed += OnPickupDestroyed;
        }

        private void OnPickupCreated(PickupBase obj)
        {
            _pickups.Add(obj);
        }

        private void OnPickupDestroyed(PickupBase obj)
        {
            _pickups.Remove(obj);
        }
    }
}
