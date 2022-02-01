using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Config/Object")]
    public class ObjectConfig : ScriptableObject
    {
        [SerializeField] private Value _speed;
        [SerializeField, Range(0, 1000)] private int _score;

        public Value Speed
        {
            get { return _speed; }
        }
        public int Score
        {
            get { return _score; }
        }
    }
}
