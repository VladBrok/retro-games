using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Pool<T> : IPool<T> where T : Destructible<T>
    {
        private readonly T _prefab;
        private readonly Transform _spawnPosition;
        private readonly Transform _parent;
        private readonly Action<T> _initialize;
        private Queue<T> _objects;

        public Pool(
            T prefab, 
            Transform spawnPosition, 
            Transform parent,
            Action<T> initialize)
        {
            _prefab = prefab;
            _spawnPosition = spawnPosition;
            _parent = parent;
            _initialize = initialize;
            _objects = new Queue<T>();
        }

        public event Action<T> ObjectDestroyed = delegate { };

        public T Get()
        {
            return Get(GetSpawnPosition());
        }

        public void Get(int count, Vector2 spawnPosition)
        {
            for (int i = 0; i < count; i++)
            {
                Get(spawnPosition);
            }
        }

        public T Get(Vector2 spawnPosition)
        {
            if (_objects.Count == 0) return InstantiateNew(spawnPosition);

            T obj = _objects.Dequeue();
            Activate(obj, spawnPosition);
            return obj;
        }

        public void Return(T obj)
        {
            obj.Deactivate();
            _objects.Enqueue(obj);
        }

        private T InstantiateNew(Vector2 spawnPosition)
        {
            T obj = GameObject.Instantiate(
                _prefab, 
                _parent);
            _initialize(obj);
            obj.Destroyed += () =>
                {
                    ObjectDestroyed(obj);
                    Return(obj);
                };
            Activate(obj, spawnPosition);
            return obj;
        }

        private Vector2 GetSpawnPosition()
        {
            return _spawnPosition == null ? Vector3.zero : _spawnPosition.position;
        }

        private void Activate(T obj, Vector2 position)
        {
            obj.transform.position = position;
            obj.Activate();
        }
    }
}
