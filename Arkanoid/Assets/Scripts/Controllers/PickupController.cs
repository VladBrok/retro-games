using System;
using System.Collections.Generic;
using UnityEngine;
using Arkanoid.Pickups;
using Arkanoid.Pickups.Effects;
using Random = UnityEngine.Random;

namespace Arkanoid.Controllers
{
    public class PickupController : MonoBehaviour
    {
        [SerializeField] private Pickup _prefab;
        [SerializeField] private List<EffectBase> _effects;
        [SerializeField] [Range(1f, 100f)] private float _spawnChance;
        [SerializeField] [Range(3f, 100f)] private float _fallForce;

        private Pool<Pickup> _pool;

        public Action<Pickup> PickupSpawned = delegate { };
        public Action<Pickup> PickupDestroyed = delegate { };

        private void Awake()
        {
            Brick.Destroyed += OnBrickDestroyed;
            _pool = new Pool<Pickup>(_prefab, p => p.TriggerEntered += OnPickupTriggerEntered);
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

        private void SpawnPickup(Vector2 position)
        {
            Pickup obj = _pool.Get(position);
            obj.Initialize(_effects[Random.Range(0, _effects.Count)], _fallForce);
            obj.StartFalling();
            PickupSpawned.Invoke(obj);
        }

        private void OnPickupTriggerEntered(Pickup obj)
        {
            _pool.Return(obj);
            PickupDestroyed.Invoke(obj);
        }
    }
}
