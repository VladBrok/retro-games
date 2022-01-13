
namespace SnakeGame.Input
{
    public class KeyboardInput : IInputProvider
    {
        public float Horizontal
        {
            get { return UnityEngine.Input.GetAxisRaw("Horizontal"); }
        }

        public float Vertical
        {
            get { return UnityEngine.Input.GetAxisRaw("Vertical"); }
        }
    }
}
