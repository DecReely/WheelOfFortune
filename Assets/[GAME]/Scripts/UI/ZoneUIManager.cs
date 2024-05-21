using System;
using CriticalStrike.WheelOfFortuneMiniGame.Zone;
using UnityEngine;

namespace CriticalStrike.WheelOfFortuneMiniGame.UI
{
    public class ZoneUIManager : MonoBehaviour
    {
        private ZoneManager _zoneManager;
        
        private ZoneUISlot[] _zoneUISlots;
    
        private void Awake()
        {
            _zoneManager = FindObjectOfType<ZoneManager>();
            
            _zoneUISlots = GetComponentsInChildren<ZoneUISlot>();
        }
    
        private void Start()
        {
            HandleZoneUI();
        }
    
        public void HandleZoneUI()
        {
            int zoneMiddle = _zoneUISlots.Length / 2;
            
            int zoneSlotsToBeDisabled = (zoneMiddle) - _zoneManager.GetCurrentZone() + 1;
            zoneSlotsToBeDisabled = Math.Clamp(zoneSlotsToBeDisabled, 0, _zoneUISlots.Length);
    
            for (int i = 0; i < _zoneUISlots.Length; i++)
            {
                if (i >= _zoneUISlots.Length - zoneSlotsToBeDisabled)
                {
                    _zoneUISlots[i].ClearSlot();
                }
                else
                {
                    int zone = (_zoneUISlots.Length - i) + _zoneManager.GetCurrentZone() - zoneMiddle - 1;
                    
                    if (zone > _zoneManager.GetMaxZone())
                    {
                        _zoneUISlots[i].ClearSlot();
                    }
                    else
                    {
                        _zoneUISlots[i].SetSlot(zone);
                    }
                }
            }
        }
    }
}
