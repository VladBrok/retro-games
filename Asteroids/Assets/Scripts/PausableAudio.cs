using UnityEngine;

namespace Asteroids
{
    class PausableAudio : IPausable
    {
        private readonly AudioSource _source;

        public PausableAudio(AudioSource source)
        {
            _source = source;
        }

        public void Pause()
        {
            _source.Pause();
        }

        public void Unpause()
        {
            _source.UnPause();
        }
    }
}
