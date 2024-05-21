using CriticalStrike.WheelOfFortuneMiniGame.Core;
using CriticalStrike.WheelOfFortuneMiniGame.Zone;
using UnityEngine;
using UnityEngine.UI;

namespace CriticalStrike.WheelOfFortuneMiniGame.UI
{
    public class WheelUIManager : MonoBehaviour
    {
        [SerializeField] private Image wheelImage;
        [SerializeField] private Image indicatorImage;

        [SerializeField] private Sprite bronzeWheelSprite;
        [SerializeField] private Sprite silverWheelSprite;
        [SerializeField] private Sprite goldWheelSprite;
    
        [SerializeField] private Sprite bronzeIndicatorSprite;
        [SerializeField] private Sprite silverIndicatorSprite;
        [SerializeField] private Sprite goldIndicatorSprite;

        private void Start()
        {
            HandleWheelUI();
        }

        public void HandleWheelUI()
        {
            switch (ZoneManager.Instance.GetCurrentZoneType())
            {
                case Enums.ZoneType.Normal:
                    wheelImage.sprite = bronzeWheelSprite;
                    indicatorImage.sprite = bronzeIndicatorSprite;
                    break;
                case Enums.ZoneType.Safe:
                    wheelImage.sprite = silverWheelSprite;
                    indicatorImage.sprite = silverIndicatorSprite;
                    break;
                case Enums.ZoneType.Super:
                    wheelImage.sprite = goldWheelSprite;
                    indicatorImage.sprite = goldIndicatorSprite;
                    break;
            }
        }
    }
}