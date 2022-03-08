using System;
using UnityEngine;
using UnityEngine.UI;
using Arkanoid.Controllers;

namespace Arkanoid
{
    public class DifficultyUI : MonoBehaviour
    {
        [SerializeField] private Toggle _easyToggle;
        [SerializeField] private Toggle _mediumToggle;
        [SerializeField] private Toggle _hardToggle;
        [SerializeField] private Button _playButton;
        [SerializeField] private DifficultyController _controller;

        private void Awake()
        {
            _easyToggle.onValueChanged.AddListener(
                isOn => CheckTrue(isOn, _controller.SelectEasy));
            _mediumToggle.onValueChanged.AddListener(
                isOn => CheckTrue(isOn, _controller.SelectMedium));
            _hardToggle.onValueChanged.AddListener(
                isOn => CheckTrue(isOn, _controller.SelectHard));

            _playButton.onClick.AddListener(_controller.SaveChanges);
            _easyToggle.isOn = true;
        }

        private void CheckTrue(bool value, Action onTrue)
        {
            if (value)
            {
                onTrue();
            }
        }
    }
}
