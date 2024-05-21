using System;
using System.Collections;
using System.Collections.Generic;
using CriticalStrike.WheelOfFortune.Events;
using CriticalStrike.WheelOfFortune.Item;
using CriticalStrike.WheelOfFortune.Misc;
using CriticalStrike.WheelOfFortune.UI;
using UnityEngine;

namespace CriticalStrike.WheelOfFortune.UI
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        private InventoryUIManager _inventoryUIManager;
        private ZoneUIManager _zoneUIManager;
        private WheelUIManager _wheelUIManager;
        private WinLoseUIManager _winLoseUIManager;

        protected override void Awake()
        {
            base.Awake();

            _inventoryUIManager = GetComponent<InventoryUIManager>();
            _zoneUIManager = GetComponent<ZoneUIManager>();
            _wheelUIManager = GetComponent<WheelUIManager>();
            _winLoseUIManager = GetComponent<WinLoseUIManager>();
        }

        private void OnEnable()
        {
            EventManager.UpdateUIEvent += HandleSpinEndUI;
        }
    
        private void OnDisable()
        {
            EventManager.UpdateUIEvent -= HandleSpinEndUI;
        }

        private void HandleSpinEndUI()
        {
            _inventoryUIManager.PrintInventoryUI();
            _zoneUIManager.HandleZoneUI();
            _wheelUIManager.HandleWheelUI();
        }

        public void HandleWinScreenUI()
        {
            _winLoseUIManager.HandleWinScreenUI();
        }
        
        public void HandleLoseScreenUI()
        {
            _winLoseUIManager.HandleLoseScreenUI();
        }

        public void HandleResetGameUI()
        {
            HandleSpinEndUI();
            _winLoseUIManager.ResetWinLoseUI();
        }
    }
}