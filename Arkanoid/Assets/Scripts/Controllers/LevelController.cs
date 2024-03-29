﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkanoid.Controllers
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private MainUI _ui;
        [SerializeField] private Paddle _paddle;
        [SerializeField] private Ball _ball;
        [SerializeField] private List<Transform> _bricksInEachLevelPrefabs;

        private int[] _brickCount;
        private int _currentLevel;

        private void Start()
        {
            Brick.Destroyed += OnBrickDestroyed;
            _brickCount = GetBrickCountForEachLevel().ToArray();
            UpdateLevel();
        }

        private void OnDestroy()
        {
            Brick.Destroyed -= OnBrickDestroyed;
        }

        private void OnValidate()
        {
            Debug.Assert(
                _bricksInEachLevelPrefabs != null && _bricksInEachLevelPrefabs.Count > 0, 
                "Bricks in each level are not assigned.");
            Debug.Assert(
                GetBrickCountForEachLevel().All(c => c > 0),
                "There must be at least one brick in each level.");
        }

        private IEnumerable<int> GetBrickCountForEachLevel()
        {
            return _bricksInEachLevelPrefabs
                       .Select(b => b.GetComponentsInChildren<Brick>().Length);
        }

        private void OnBrickDestroyed(BrickDestroyedData _)
        {
            _brickCount[_currentLevel - 1]--;
            if (AllBricksInLevelDestroyed())
            {
                if (CurrentLevelIsLast())
                {
                    TogglePaddleAndBallActive(false);
                    _ui.ShowVictoryCanvas();
                    return;
                }
                UpdateLevel();
            }
        }

        private bool AllBricksInLevelDestroyed()
        {
            return _brickCount[_currentLevel - 1] == 0;
        }

        private bool CurrentLevelIsLast()
        {
            return _currentLevel == _brickCount.Length;
        }

        private void UpdateLevel()
        {
            _currentLevel++;
            ResetPaddleAndBall();
            StartCoroutine(UpdateLevelRoutine());
        }

        private IEnumerator UpdateLevelRoutine()
        {
            TogglePaddleAndBallActive(false);
            yield return _ui.UpdateLevelRoutine(_currentLevel);
            InstantiateNewBricks();
            TogglePaddleAndBallActive(true);
        }

        private void ResetPaddleAndBall()
        {
            _paddle.Reset();
            _ball.Reset();
        }

        private void TogglePaddleAndBallActive(bool active)
        {
            _paddle.gameObject.SetActive(active);
            _ball.gameObject.SetActive(active);
        }

        private void InstantiateNewBricks()
        {
            Instantiate(_bricksInEachLevelPrefabs[_currentLevel - 1]);
        }
    }
}