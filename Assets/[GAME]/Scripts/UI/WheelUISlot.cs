using System;
using System.Collections;
using System.Collections.Generic;
using CriticalStrike.WheelOfFortune.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace CriticalStrike.WheelOfFortune.UI
{
    [ExecuteInEditMode]
    public class WheelUISlot : MonoBehaviour
    {
        private Image _icon;
        private TextMeshProUGUI _quantityText;
        
        [SerializeField] private Wheel.WheelOfFortune.Reward _currentReward;

        private void Awake()
        {
            _icon = GetComponentInChildren<Image>();
            _quantityText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnValidate()
        {
            ValidateReward();
            
            SetCurrentReward(_currentReward);
        }

        private void ValidateReward()
        {
            if (_currentReward.Quantity < 0)
            {
                int quantity = Math.Clamp(_currentReward.Quantity, 0, int.MaxValue);
                
                _currentReward.Quantity = quantity;
            }
        }

        private void FillSlot(Wheel.WheelOfFortune.Reward reward)
        {
            if (reward.Item == null)
                return;
            if (_icon == null)
                return;
            if (_quantityText == null)
                return;
            
            _icon.sprite = reward.Item.Icon;

            if (reward.Quantity == 0)
            {
                _quantityText.SetText("");
            }
            else
            {
                _quantityText.SetText($"x{reward.Quantity}");
            }
        }

        public void SetCurrentReward(Wheel.WheelOfFortune.Reward reward)
        {
            _currentReward = reward;

            FillSlot(_currentReward);
        }

        public Wheel.WheelOfFortune.Reward GetCurrentReward()
        {
            return _currentReward;
        }
    }
}

