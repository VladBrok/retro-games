using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Config/Asteroid")]
    public class AsteroidConfig : ObjectConfig
    {
        [SerializeField] private AsteroidType _type;
        [SerializeField] private Asteroid _prefab;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private Value _spawnOffsetPercent;

        private ReadOnlyCollection<Sprite> _readonlySprites;
        private Value _spawnOffsetX;
        private Value _spawnOffsetY;

        public AsteroidType Type
        {
            get { return _type; }
        }
        public Asteroid Prefab
        {
            get { return _prefab; }
        }
        public ReadOnlyCollection<Sprite> Sprites
        {
            get { return _readonlySprites; }
        }
        public Value SpawnOffsetPercent
        {
            get { return _spawnOffsetPercent; }
        }
        public Value SpawnOffsetX
        {
            get { return _spawnOffsetX; }
        }
        public Value SpawnOffsetY
        {
            get { return _spawnOffsetY; }
        }

        public void Initialize(Value spawnOffsetX, Value spawnOffsetY)
        {
            _spawnOffsetX = spawnOffsetX;
            _spawnOffsetY = spawnOffsetY;
            _readonlySprites = Array.AsReadOnly(_sprites);
        }
    }
}
