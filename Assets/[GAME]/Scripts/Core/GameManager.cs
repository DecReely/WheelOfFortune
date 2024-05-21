using System;
using System.Collections;
using System.Collections.Generic;
using CriticalStrike.WheelOfFortune.Events;
using CriticalStrike.WheelOfFortune.Item;
using CriticalStrike.WheelOfFortune.Misc;
using CriticalStrike.WheelOfFortune.UI;
using CriticalStrike.WheelOfFortune.Zone;
using UnityEngine;

namespace CriticalStrike.WheelOfFortune.Core
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        private bool _isGameEnded;
        
        private void OnEnable()
        {
            EventManager.SpinEndedEvent += HandleSpinEnd;
        }

        private void OnDisable()
        {
            EventManager.SpinEndedEvent -= HandleSpinEnd;
        }

        private void HandleSpinEnd()
        {
            WheelOfFortune.Wheel.WheelOfFortune.Instance.HandleLanding();
        }

        public void HandleGameState()
        {
            if (ZoneManager.Instance.IsMaxLevel())
            {
                HandleGameWin();
            }
            else if (PlayerInventory.Instance.DoesInventoryContainDeath())
            {
                HandleGameLose();
            }
            else
            {
                ZoneManager.Instance.IncrementCurrentZone();
            }
        }

        public void HandleGameWin()
        {
            EndGame();
            UIManager.Instance.HandleWinScreenUI();
            Debug.Log("Game Won!");
        }

        public void HandleGameLose()
        {
            EndGame();
            UIManager.Instance.HandleLoseScreenUI();
            Debug.Log("Game Lost!");
        }

        public bool IsGameEnded()
        {
            return _isGameEnded;
        }

        public void EndGame()
        {
            _isGameEnded = true;
        }

        public void ResetGame()
        {
            _isGameEnded = false;
            
            ZoneManager.Instance.ResetCurrentZone();
            PlayerInventory.Instance.ResetInventory();
            UIManager.Instance.HandleResetGameUI();
        }
    }
}