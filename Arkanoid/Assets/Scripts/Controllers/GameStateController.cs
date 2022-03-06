using UnityEngine;
using System.Reflection;

namespace Arkanoid
{
    public sealed class GameStateController : MonoBehaviour
    {
        [SerializeField] private Paddle _paddle;
        [SerializeField] private Ball _ball;

        public void Pause()
        {
            _paddle.Pause();
            _ball.Pause();
        }

        public void Unpause()
        {
            _paddle.Unpause();
            _ball.Unpause();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
