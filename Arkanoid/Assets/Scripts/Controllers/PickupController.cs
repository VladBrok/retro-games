using System.Collections.Generic;
using UnityEngine;
using Arkanoid.Pickups;

namespace Arkanoid.Controllers
{
    public class PickupController : MonoBehaviour
    {
        [SerializeField] private List<PickupBase> _pickupPrefabs;
        [SerializeField] private PickupConfig _config;
        [SerializeField] [Range(1f, 100f)] private float _spawnChance;

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
                PickupBase pickup = Instantiate(
                    _pickupPrefabs[Random.Range(0, _pickupPrefabs.Count)], 
                    data.Position, 
                    Quaternion.identity);
                pickup.Initialize(_config);
                pickup.TriggerEntered += OnPickupTriggerEntered;
            }
        }

        private void OnPickupTriggerEntered(PickupBase target)
        {
            // TODO: Create pool
            Destroy(target.gameObject);
        }
    }
}
