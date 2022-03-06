using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkanoid
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private UI _ui;
        [SerializeField] private Paddle _paddle;
        [SerializeField] private Ball _ball;
        [SerializeField] private List<Transform> _bricksInEachLevelPrefabs;

        private int[] _brickCount;
        private int _currentLevel;

        private void Start()
        {
            Brick.Destroyed += OnBrickDestroyed;
            _brickCount = _bricksInEachLevelPrefabs
                .Select(b => b.GetComponentsInChildren<Brick>().Count())
                .ToArray();
            Debug.Assert(
                Array.TrueForAll(_brickCount, c => c > 0), 
                "There must be at least one brick in each level.");
            StartCoroutine(UpdateLevelRoutine());
        }

        private void OnDestroy()
        {
            Brick.Destroyed -= OnBrickDestroyed;
        }

        private void OnValidate()
        {
            Debug.Assert(
                _bricksInEachLevelPrefabs != null && _bricksInEachLevelPrefabs.Count > 0, 
                "Bricks in level are not assigned.");
        }

        private void OnBrickDestroyed()
        {
            if (--_brickCount[_currentLevel - 1] == 0)
            {
                StartCoroutine(UpdateLevelRoutine());
            }
        }

        private IEnumerator UpdateLevelRoutine()
        {
            _paddle.Reset();
            _ball.Reset();
            _paddle.gameObject.SetActive(false);
            _currentLevel++;
            yield return _ui.UpdateLevelRoutine(_currentLevel);
            Instantiate(_bricksInEachLevelPrefabs[_currentLevel - 1]);
            _paddle.gameObject.SetActive(true);
        }
    }
}