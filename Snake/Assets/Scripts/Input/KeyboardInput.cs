using UnityEngine;

namespace SnakeGame.Input
{
    public class KeyboardInput : IMovementInput, IPauseInput
    {
        public float Horizontal
        {
            get { return UnityEngine.Input.GetAxisRaw("Horizontal"); }
        }

        public float Vertical
        {
            get { return UnityEngine.Input.GetAxisRaw("Vertical"); }
        }

        public bool Pause
        {
            get { return UnityEngine.Input.GetKeyDown(KeyCode.P); }
        }
    }
}
