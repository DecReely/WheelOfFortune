using System.Collections;
using System.Collections.Generic;
using CriticalStrike.WheelOfFortune.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CriticalStrike.WheelOfFortune.UI
{
    public class RestartButton : MonoBehaviour
    {
        private Button _button;
    
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(Restart);
        }

        private void Restart()
        {
            GameManager.Instance.ResetGame();
        }
    }
}
