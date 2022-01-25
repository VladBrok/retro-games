using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Pool<T> : IPool<T> 
        where T : Destructible<T>
    {
        private readonly T _prefab;
        private readonly Transform _spawnPoint;
        private readonly Transform _parent;
        private readonly Action<T> _initialize;
        private Queue<T> _objects;

        public Pool(
            T prefab, 
            Transform spawnPoint, 
            Transform parent,
            Action<T> initialize)
        {
            _prefab = prefab;
            _spawnPoint = spawnPoint;
            _parent = parent;
            _initialize = initialize;
            _objects = new Queue<T>();
        }

        public T Get()
        {
            if (_objects.Count == 0)
            {
                return InstantiateNew();
            }
            T obj = _objects.Dequeue();
            obj.transform.position = GetSpawnPosition();
            obj.Show();
            return obj;
        }

        public void Return(T obj)
        {
            obj.Hide();
            _objects.Enqueue(obj);
        }

        private T InstantiateNew()
        {
            T obj = GameObject.Instantiate(
                _prefab, 
                GetSpawnPosition(),
                Quaternion.identity, 
                _parent);
            _initialize(obj);
            obj.Destroyed += () => Return(obj);
            obj.Show();
            return obj;
        }

        private Vector2 GetSpawnPosition()
        {
            return _spawnPoint == null ? Vector3.zero : _spawnPoint.position;
        }
    }
}
