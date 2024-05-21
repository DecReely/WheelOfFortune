using System;
using System.Collections;
using System.Collections.Generic;
using CriticalStrike.WheelOfFortune.Core;
using CriticalStrike.WheelOfFortune.Events;
using CriticalStrike.WheelOfFortune.Wheel;
using CriticalStrike.WheelOfFortune.Zone;
using UnityEngine;
using UnityEngine.UI;

namespace CriticalStrike.WheelOfFortune.UI
{
    public class ExitButton : MonoBehaviour
    {
        private Button _button;
    
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(Exit);
        }

        private void Exit()
        {
            if ( !Wheel.WheelOfFortune.Instance.IsRotating() &&
                 (ZoneManager.Instance.GetCurrentZoneType() == Enums.ZoneType.Safe || ZoneManager.Instance.GetCurrentZoneType() == Enums.ZoneType.Super))
            {
                GameManager.Instance.HandleGameWin();
            }
            else
            {
                // TODO: Implement tooltip here.
            }
        }
    }
}