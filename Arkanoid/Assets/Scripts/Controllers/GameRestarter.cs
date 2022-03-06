using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkanoid
{
    public class GameRestarter : MonoBehaviour
    {
        private const int MainSceneIndex = 0;

        public void Restart()
        {
            SceneManager.LoadScene(MainSceneIndex);
        }
    }
}
