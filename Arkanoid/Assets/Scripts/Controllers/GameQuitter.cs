using UnityEngine;

namespace Arkanoid.Controllers
{
    public class GameQuitter : MonoBehaviour
    {
        public void Quit()
        {
            Application.Quit();
        }
    }
}
