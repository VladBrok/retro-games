﻿using System;
using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(BoxCollider2D))]
    [DisallowMultipleComponent]
    public abstract class Destructible<T> : MonoBehaviour, IDestructible, IActivable
        where T : IWrapable
    {
        private BoxCollider2D _collider;
        private WraparoundBase<T> _wraparound;

        public event Action Destroyed = delegate { };

        public Vector2 Center
        {
            get { return transform.position; }
            set { transform.position = value; }
        }
        public Vector2 Extents
        {
            get { return _collider.bounds.extents; }
        }

        public void Initialize(WraparoundBase<T> wraparound)
        {
            _wraparound = wraparound;
        }

        public virtual void Activate()
        {
            gameObject.SetActive(false);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(true);
        }

        public bool Intersects(Bounds other)
        {
            return _collider.bounds.Intersects(other);
        }

        public virtual void Destroy()
        {
            Destroyed();
        }

        protected virtual void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            Debug.Assert(
                _collider.isTrigger,
                "BoxCollider2D of the " + gameObject.name + " should be a trigger.");
        }

        protected virtual void Update()
        {
            _wraparound.Update();
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<IDestructible>().Destroy();
        }
    }
}
