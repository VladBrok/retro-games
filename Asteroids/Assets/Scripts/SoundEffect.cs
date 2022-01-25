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

        public SoundEffectType Type
        {
            get { return _type; }
        }
        public ReadOnlyCollection<AudioClip> Clips
        {
            get { return Array.AsReadOnly(_clips); }
        }
    }
}
