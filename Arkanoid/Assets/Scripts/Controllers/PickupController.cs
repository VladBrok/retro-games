using System;
using System.Collections.Generic;
using UnityEngine;
using Arkanoid.Pickups;
using Random = UnityEngine.Random;

namespace Arkanoid.Controllers
{
    public class PickupController : MonoBehaviour
    {
        [SerializeField] private List<PickupBase> _pickupPrefabs;
        [SerializeField] private PickupConfig _config;
        [SerializeField] [Range(1f, 100f)] private float _spawnChance;

        public Action<PickupBase> PickupCreated = delegate { };
        public Action<PickupBase> PickupDestroyed = delegate { };

        private void Awake()
        {
            Brick.Destroyed += OnBrickDestroyed;
        }

        private void OnDestroy()
        {
            Brick.Destroyed -= OnBrickDestroyed;
        }

        private void OnBrickDestroyed(BrickDestroyedData data)
        {
            if (Random.Range(1f, 100f) <= _spawnChance)
            {
                PickupBase pickup = CreatePickup(data.Position);
                pickup.TriggerEntered += OnPickupTriggerEntered;
            }
        }

        private PickupBase CreatePickup(Vector2 position)
        {
            PickupBase pickup = Instantiate(
                _pickupPrefabs[Random.Range(0, _pickupPrefabs.Count)],
                position,
                Quaternion.identity);
            pickup.Initialize(_config);
            PickupCreated.Invoke(pickup);
            return pickup;
        }

        private void OnPickupTriggerEntered(PickupBase obj)
        {
            Destroy(obj.gameObject);
            PickupDestroyed.Invoke(obj);
        }
    }
}
