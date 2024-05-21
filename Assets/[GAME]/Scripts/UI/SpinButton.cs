using System.Collections;
using System.Collections.Generic;
using CriticalStrike.WheelOfFortune.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CriticalStrike.WheelOfFortune.UI
{
    public class SpinButton : MonoBehaviour
    {
        private Button _button;
    
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(Spin);
        }

        private void Spin()
        {
            Wheel.WheelOfFortune.Instance.Rotate();
        }
    }
}
