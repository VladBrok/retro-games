using System;
using System.Collections;
using UnityEngine;

namespace SnakeGame.MonoBehaviours
{
    public class GameField : MonoBehaviour
    {
        private TriggerBody _food;
        private Field _field;

        public void Initialize(Field field, Respawner respawner, TriggerBody food)
        {
            _field = field;
            _food = food;
            _food.TriggerEntered += respawner.RespawnTarget;

            respawner.BeforeRespawn = () => StartCoroutine(HideFoodTemporarly());
            respawner.RespawnTarget();
        }

        private IEnumerator HideFoodTemporarly()
        {
            _food.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            _food.gameObject.SetActive(true);
        }

        private void Update()
        {
            _field.Update();
        }
    }
}
