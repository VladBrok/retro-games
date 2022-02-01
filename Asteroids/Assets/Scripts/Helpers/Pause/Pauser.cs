using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids
{
    [DisallowMultipleComponent]
    public class Pauser : MonoBehaviour
    {
        private IPauseInput _input;
        private List<IPausable> _pausables;
        private bool _paused;

        public void Initialize(IPauseInput input, params IPausable[] pausables)
        {
            _input = input;
            _pausables = new List<IPausable>(pausables);
        }

        private void Update()
        {
            if (!_input.Pause) return;

            if (_paused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }

        private void Pause()
        {
            Time.timeScale = 0f;
            _paused = true;
            _pausables.ForEach(p => p.Pause());
        }

        private void Unpause()
        {
            Time.timeScale = 1f;
            _paused = false;
            _pausables.ForEach(p => p.Unpause());
        }
    }
}
