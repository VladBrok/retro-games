using UnityEngine;

namespace Asteroids
{
    public class KeyboardInput : IRespawnInput, IShipInput, IWeaponInput, IPauseInput
    {
        public bool Respawn
        {
            get 
            {
                return Input.GetKeyDown(KeyCode.LeftShift) 
                       || Input.GetKeyDown(KeyCode.RightShift); 
            }
        }

        public bool Fire
        {
            get { return Input.GetKeyDown(KeyCode.Space); }
        }

        public bool Accelerating
        {
            get { return Input.GetKey(KeyCode.W); }
        }

        public float TurnDirection
        {
            get 
            {
                return Input.GetKey(KeyCode.A) ? 1f :
                       Input.GetKey(KeyCode.D) ? -1f :
                       0f; 
            }
        }

        public bool Pause
        {
            get { return Input.GetKeyDown(KeyCode.P); }
        }
    }
}
