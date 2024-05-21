using CriticalStrike.WheelOfFortuneMiniGame.Core;
using CriticalStrike.WheelOfFortuneMiniGame.Zone;
using UnityEngine;
using UnityEngine.UI;

namespace CriticalStrike.WheelOfFortuneMiniGame.UI
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

        private static void Exit()
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