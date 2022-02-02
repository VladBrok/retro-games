using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Config/SoundEffect")]
    public class SoundEffectConfig : ScriptableObject
    {
        [SerializeField] private SoundEffectType _type;
        [SerializeField] private AudioClip[] _clips;
        [SerializeField] [Range(0f, 1f)] private float _volumeScale;

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
        public float VolumeScale
        {
            get { return _volumeScale; }
        }
    }
}
