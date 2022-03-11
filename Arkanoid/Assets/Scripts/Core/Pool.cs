using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    public class Pool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Action<T> _initialize;
        private readonly Queue<T> _objects;

        public Pool(T prefab, Action<T> initialize)
        {
            _prefab = prefab;
            _initialize = initialize;
            _objects = new Queue<T>();
        }

        public T Get(Vector2 position)
        {
            if (_objects.Count == 0)
            {
                return CreateNew(position);
            }
            return GetExisting(position);
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            _objects.Enqueue(obj);
        }

        private T CreateNew(Vector2 position)
        {
            T obj = GameObject.Instantiate(_prefab, position, Quaternion.identity);
            _initialize(obj);
            return obj;
        }

        private T GetExisting(Vector2 position)
        {
            T obj = _objects.Dequeue();
            obj.gameObject.SetActive(true);
            obj.transform.position = position;
            return obj;
        }
    }
}
