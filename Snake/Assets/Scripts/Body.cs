using UnityEngine;

namespace SnakeGame
{
    public class Body
    {
        private readonly IPositionProvider _target;
        private readonly Vector2 _size;

        public Body(IPositionProvider target, Vector2 size)
        {
            _target = target;
            _size = size;
        }

        public Vector2 Position 
        {
            get 
            { 
                return _target.Position; 
            }
            set 
            { 
                _target.Position = value; 
            } 
        }
        public Vector2 Size
        {
            get 
            { 
                return _size; 
            }
        }
    }
}