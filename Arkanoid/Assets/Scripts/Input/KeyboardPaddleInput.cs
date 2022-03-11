using UnityEngine;

namespace Arkanoid.Input
{
    public class KeyboardPaddleInput : IPaddleInput
    {
        public Vector2 MovementDirection
        {
            get
            {
                return UnityEngine.Input.GetKey(KeyCode.A) ? Vector2.left :
                       UnityEngine.Input.GetKey(KeyCode.D) ? Vector2.right :
                       Vector2.zero;
            }
        }
        public bool LaunchBall 
        {
            get { return UnityEngine.Input.GetKeyDown(KeyCode.Space); }
        }
    }
}
