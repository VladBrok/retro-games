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

        private Pool<PickupBase> _pool;

        public Action<PickupBase> PickupSpawned = delegate { };
        public Action<PickupBase> PickupDestroyed = delegate { };

        private void Awake()
        {
            Brick.Destroyed += OnBrickDestroyed;
            _pool = new Pool<PickupBase>(InitializePickup);
        }

        private void OnDestroy()
        {
            Brick.Destroyed -= OnBrickDestroyed;
        }

        private void OnBrickDestroyed(BrickDestroyedData data)
        {
            if (Random.Range(1f, 100f) <= _spawnChance)
            {
                SpawnPickup(data.Position);
            }
        }

        private void InitializePickup(PickupBase obj)
        {
            obj.TriggerEntered += OnPickupTriggerEntered;
            obj.Initialize(_config);
        }

        private void SpawnPickup(Vector2 position)
        {
            PickupBase pickup = _pool.Get(
                _pickupPrefabs[Random.Range(0, _pickupPrefabs.Count)],
                position);
            pickup.StartFalling();
            PickupSpawned.Invoke(pickup);
        }

        private void OnPickupTriggerEntered(PickupBase obj)
        {
            _pool.Return(obj);
            PickupDestroyed.Invoke(obj);
        }
    }
}
