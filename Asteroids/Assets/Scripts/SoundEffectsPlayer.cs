using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffectsPlayer : MonoBehaviour
    {
        [SerializeField] private SoundEffect[] _effects;

        private AudioSource _audioSource;

        public bool IsPlaying
        {
            get { return _audioSource.isPlaying; }
        }

        public void PlayOneShot(SoundEffectType type)
        {
            var clips = _effects.Single(effect => effect.Type == type).Clips;
            int randomIndex = Random.Range(0, clips.Count);
            _audioSource.PlayOneShot(clips[randomIndex]);
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnValidate()
        {
            Debug.Assert(
                _effects.Length == _effects.Select(effect => effect.Type).Distinct().Count(),
                "All types of sound effects should be distinct.");
        }
    }
}
