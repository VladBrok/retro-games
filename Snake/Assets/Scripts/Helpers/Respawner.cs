using System;
using System.Linq;
using UnityEngine;
using SnakeGame.Controllers;
using Random = UnityEngine.Random;

namespace SnakeGame.Helpers
{
    public class Respawner
    {
        private readonly IBody _target;
        private readonly IEmptyPositionsProvider _positionsProvider;
        private Action _prepareToRespawn;

        public Respawner(IBody target, 
                         IEmptyPositionsProvider positionsProvider, 
                         Action prepareToRespawn)
        {
            _target = target;
            _positionsProvider = positionsProvider;
            _prepareToRespawn = prepareToRespawn;
        }

        public void RespawnTarget()
        {
            if (!_positionsProvider.EmptyPositions.Any())
            {
                return;
            }
            _prepareToRespawn();
            _target.Position = GetRandomPosition();
        }

        private Vector2 GetRandomPosition()
        {
            int randIndex = Random.Range(0, _positionsProvider.EmptyPositions.Count());
            return _positionsProvider.EmptyPositions.ElementAt(randIndex);
        }
    }
}
