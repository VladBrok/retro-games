using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Config/Asteroid")]
    public class AsteroidConfig : ScriptableObject
    {
        [SerializeField] private AsteroidType _type;
        [SerializeField] private Asteroid _prefab;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private Value _spawnOffsetPercent;
        [SerializeField] private Value _speed;
        [SerializeField] private int _score;

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
        public Value Speed
        {
            get { return _speed; }
        }
        public Value SpawnOffsetX
        {
            get { return _spawnOffsetX; }
        }
        public Value SpawnOffsetY
        {
            get { return _spawnOffsetY; }
        }
        public int Score
        {
            get { return _score; }
        }

        public void Initialize(Value spawnOffsetX, Value spawnOffsetY)
        {
            _spawnOffsetX = spawnOffsetX;
            _spawnOffsetY = spawnOffsetY;
            _readonlySprites = Array.AsReadOnly(_sprites);
        }
    }
}
