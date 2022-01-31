using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public class SoundEffect
    {
        [SerializeField] private SoundEffectType _type;
        [SerializeField] private AudioClip[] _clips;

        private ReadOnlyCollection<AudioClip> _readonlyClips;

        public SoundEffectType Type
        {
            get { return _type; }
        }
        public ReadOnlyCollection<AudioClip> Clips
        {
            get 
            {
                if (_readonlyClips == null)
                {
                    _readonlyClips = Array.AsReadOnly(_clips);
                }
                return _readonlyClips; 
            }
        }
    }
}
