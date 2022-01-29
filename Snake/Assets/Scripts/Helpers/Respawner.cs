using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using SnakeGame.Controllers;

namespace SnakeGame.Helpers
{
    public class Respawner
    {
        private readonly IBody _target;
        private readonly IEmptyPositionsProvider _positionsProvider;
        private Action _beforeRespawn;

        public Respawner(IBody target, 
                         IEmptyPositionsProvider positionsProvider, 
                         Action beforeRespawn)
        {
            _target = target;
            _positionsProvider = positionsProvider;
            _beforeRespawn = beforeRespawn;
        }

        public void RespawnTarget()
        {
            if (!_positionsProvider.EmptyPositions.Any())
            {
                return;
            }
            _beforeRespawn();
            _target.Position = GetRandomPosition();
        }

        private Vector2 GetRandomPosition()
        {
            int randIndex = Random.Range(0, _positionsProvider.EmptyPositions.Count());
            return _positionsProvider.EmptyPositions.ElementAt(randIndex);
        }
    }
}
