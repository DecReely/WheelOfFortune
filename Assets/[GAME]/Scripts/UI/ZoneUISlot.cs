using System;
using System.Collections;
using System.Collections.Generic;
using CriticalStrike.WheelOfFortune.Core;
using CriticalStrike.WheelOfFortune.Zone;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CriticalStrike.WheelOfFortune.UI
{
    public class ZoneUISlot : MonoBehaviour
    {
        private ZoneManager _zoneManager;
        
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image borderImage;
        [SerializeField] private TextMeshProUGUI zoneText;
    
        [Range(0, 255)] [SerializeField] private int nonCurrentZoneBorderImageAlphaValue;
    
        [SerializeField] private Sprite normalZoneBackground;
        [SerializeField] private Sprite safeZoneBackground;
        [SerializeField] private Sprite superZoneBackground;
        [SerializeField] private Sprite currentZoneBorderSprite;
        [SerializeField] private Sprite nonCurrentZoneBorderSprite;
    
        
        // May add text color variations for different zone types here.
    
        private void Awake()
        {
            _zoneManager = FindObjectOfType<ZoneManager>();
        }
    
        public void ClearSlot()
        {
            backgroundImage.enabled = false;
            borderImage.enabled = false;
            zoneText.SetText("");
        }
    
        public void SetSlot(int zone)
        {
            Enums.ZoneType zoneType = _zoneManager.GetZoneType(zone);
            bool isCurrentZone = zone == _zoneManager.GetCurrentZone();
            
            backgroundImage.enabled = true;
            borderImage.enabled = true;
            
            switch (zoneType)
            {
                case Enums.ZoneType.Normal:
                    backgroundImage.sprite = normalZoneBackground;
                    break;
                case Enums.ZoneType.Safe:
                    backgroundImage.sprite = safeZoneBackground;
                    break;
                case Enums.ZoneType.Super:
                    backgroundImage.sprite = superZoneBackground;
                    break;
            }
            
            zoneText.SetText(zone.ToString());
            
            if (isCurrentZone)
            {
                backgroundImage.color = Color.white;
                borderImage.sprite = currentZoneBorderSprite;
                borderImage.color = Color.white;
            }
            else
            {
                backgroundImage.color = new Color(255, 255, 255, nonCurrentZoneBorderImageAlphaValue);
                borderImage.sprite = nonCurrentZoneBorderSprite;
                backgroundImage.color = new Color(255, 255, 255, nonCurrentZoneBorderImageAlphaValue);
            }
        }
    }
}