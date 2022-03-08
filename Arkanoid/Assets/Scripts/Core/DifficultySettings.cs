using UnityEngine;

namespace Arkanoid
{
    [CreateAssetMenu(menuName = "Settings/Difficulty")]
    public sealed class DifficultySettings : ScriptableObject
    {
        [SerializeField] [Range(0.1f, 20f)] private float _ballSpeed;
        [SerializeField] [Range(0.1f, 10f)] private float _paddleWidthMultiplier;
        [SerializeField] [HideInInspector] private bool _selected;

        public float BallSpeed { get { return _ballSpeed; } }
        public float PaddleWidthMultiplier { get { return _paddleWidthMultiplier; } }
        public bool Selected 
        {
            get { return _selected; }
            set { _selected = value; }
        }
    }
}
