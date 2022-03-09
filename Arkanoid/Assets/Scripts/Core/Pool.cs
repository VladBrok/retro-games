using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkanoid
{
    public class Pool<T> where T : Component
    {
        private readonly List<T> _objects;
        private readonly Action<T> _initialize;

        public Pool(Action<T> initialize)
        {
            _objects = new List<T>();
            _initialize = initialize;
        }

        public T Get(T prefab, Vector2 position)
        {
            T obj = _objects.FirstOrDefault(o => o.GetType().Equals(prefab.GetType()));
            if (obj == default(T))
            {
                return CreateNew(prefab, position);
            }
            return GetExisting(obj, position);
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            _objects.Add(obj);
        }

        private T CreateNew(T prefab, Vector2 position)
        {
            T obj = GameObject.Instantiate(prefab, position, Quaternion.identity);
            _initialize(obj);
            return obj;
        }

        private T GetExisting(T obj, Vector2 position)
        {
            _objects.Remove(obj);
            obj.gameObject.SetActive(true);
            obj.transform.position = position;
            return obj;
        }
    }
}
