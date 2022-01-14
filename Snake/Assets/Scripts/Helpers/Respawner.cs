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
        private readonly IEmptyPositionsProvider _helper;
        private Action _beforeRespawn;

        public Respawner(IBody target, 
                         IEmptyPositionsProvider positionsProvider, 
                         Action beforeRespawn)
        {
            _target = target;
            _helper = positionsProvider;
            _beforeRespawn = beforeRespawn;
        }

        public void RespawnTarget()
        {
            _beforeRespawn();
            _target.Position = GetRandomPosition();
        }

        private Vector2 GetRandomPosition()
        {
            if (!_helper.EmptyPositions.Any())
            {
                return new Vector2();
            }

            int randIndex = Random.Range(0, _helper.EmptyPositions.Count());
            return _helper.EmptyPositions.ElementAt(randIndex);
        }
    }
}
