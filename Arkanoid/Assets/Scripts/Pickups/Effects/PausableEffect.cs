using System;
using System.Collections;
using UnityEngine;

namespace Arkanoid.Pickups.Effects
{
    public abstract class PausableEffect : EffectBase, IPausable
    {
        private WaitWhile _waitWhilePaused;
        private bool _paused;

        public void Pause()
        {
            _paused = true;
        }

        public void Unpause()
        {
            _paused = false;
        }

        protected virtual void Awake()
        {
            _waitWhilePaused = new WaitWhile(() => _paused);
        }

        protected IEnumerator ExecuteWhileNotPausedRoutine(float duration, Action execute)
        {
            while (duration > 0f)
            {
                if (_paused) yield return _waitWhilePaused;
                execute();
                duration = Mathf.Max(0f, duration - Time.deltaTime);
                yield return null;
            }
        }
    }
}
