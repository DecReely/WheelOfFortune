using System.Collections;
using System.Collections.Generic;
using CriticalStrike.WheelOfFortune.Core;
using CriticalStrike.WheelOfFortune.Events;
using CriticalStrike.WheelOfFortune.Misc;
using CriticalStrike.WheelOfFortune.Wheel;
using UnityEngine;

namespace CriticalStrike.WheelOfFortune.Zone
{
    public class ZoneManager : SingletonMonoBehaviour<ZoneManager>
    {
        private const int MaxZone = 60;
        private int _currentZone = 1;
    
        public int GetCurrentZone()
        {
            return _currentZone;
        }
    
        public void IncrementCurrentZone()
        {
            _currentZone++;
            
            WheelOfFortune.Wheel.WheelOfFortune.Instance.RandomizeSlotRewards();
            
            EventManager.CallUpdateUIEvent();
        }
    
        public void ResetCurrentZone()
        {
            _currentZone = 1;
            
            WheelOfFortune.Wheel.WheelOfFortune.Instance.RandomizeSlotRewards();
            
            EventManager.CallUpdateUIEvent();
        }
    
        public Enums.ZoneType GetZoneType(int zone)
        {
            if (zone % 30 == 0)
            {
                return Enums.ZoneType.Super;
            }
            else if (zone % 5 == 0)
            {
                return Enums.ZoneType.Safe;
            }
            else
            {
                return Enums.ZoneType.Normal;
            }
        }
        
        public Enums.ZoneType GetCurrentZoneType()
        { 
            return GetZoneType(_currentZone);
        }
    
        public int GetMaxZone()
        {
            return MaxZone;
        }
    
        public bool HasExceededMaxLevel()
        {
            return _currentZone > MaxZone;
        }

        public bool IsMaxLevel()
        {
            return _currentZone == MaxZone;
        }
    }
}