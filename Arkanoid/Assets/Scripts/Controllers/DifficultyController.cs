using UnityEngine;

namespace Arkanoid.Controllers
{
    public class DifficultyController : MonoBehaviour
    {
        [SerializeField] private DifficultySettings _easy;
        [SerializeField] private DifficultySettings _medium;
        [SerializeField] private DifficultySettings _hard;

        private DifficultySettings _selected;

        public void SaveChanges()
        {
            _selected.Selected = true;
        }

        public void SelectEasy()
        {
            _selected = _easy;
        }

        public void SelectMedium()
        {
            _selected = _medium;
        }

        public void SelectHard()
        {
            _selected = _hard;
        }

        private void Awake()
        {
            foreach (var difficulty in new[] { _easy, _medium, _hard })
            {
                difficulty.Selected = false;
            }
        }
    }
}
