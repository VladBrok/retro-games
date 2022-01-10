using UnityEngine;

namespace SnakeGame
{
    public class KeyboardInput : IInputProvider
    {
        public float Horizontal
        {
            get 
            {
                return Input.GetAxisRaw("Horizontal"); 
            }
        }

        public float Vertical
        {
            get 
            { 
                return Input.GetAxisRaw("Vertical"); 
            }
        }
    }
}
