using UnityEngine;

namespace Asteroids
{
    [DisallowMultipleComponent]
    public sealed class FPSLimiter : MonoBehaviour
    {
        [SerializeField] [Range(0, 300)] private int _limit;

        private int _defaultVSyncCount;

        private void Awake()
        {
            _defaultVSyncCount = QualitySettings.vSyncCount;
        }

        private void OnEnable()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = _limit;
        }

        private void OnDisable()
        {
            QualitySettings.vSyncCount = _defaultVSyncCount;
            Application.targetFrameRate = -1;
        }
    }
}
