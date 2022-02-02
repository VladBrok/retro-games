using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class SoundEffectsPlayer : MonoBehaviour, ISoundEffectsPlayer
    {
        [SerializeField] private SoundEffectConfig[] _effects;

        private AudioSource _audioSource;
        private Queue<SoundEffectType> _pendingClips;

        public void PlayOneShot(SoundEffectType type)
        {
            if (_pendingClips.Contains(type)) return;

            _pendingClips.Enqueue(type);
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _pendingClips = new Queue<SoundEffectType>();
        }

        private void LateUpdate()
        {
            if (_pendingClips.Count == 0) return;

            PlayOneShot();
        }

        private void PlayOneShot()
        {
            SoundEffectType type = _pendingClips.Dequeue();
            SoundEffectConfig config = _effects.First(e => e.Type == type);
            int randomIndex = Random.Range(0, config.Clips.Count);
            _audioSource.PlayOneShot(config.Clips[randomIndex], config.VolumeScale);
        }

        private void OnValidate()
        {
            Debug.Assert(
                _effects.Length == _effects.Select(e => e.Type).Distinct().Count(),
                "All types of sound effects should be distinct.");
        }
    }
}
