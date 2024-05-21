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
    public class InventoryUISlot : MonoBehaviour
    {
        private Image _icon;
        private TextMeshProUGUI _quantityText;
        
        private Wheel.WheelOfFortune.Reward _reward;

        private void Awake()
        {
            _icon = GetComponentInChildren<Image>();
            _quantityText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void FillSlot(Wheel.WheelOfFortune.Reward reward)
        {
            _icon.sprite = reward.Item.Icon;
            _quantityText.SetText($"x{reward.Quantity}");
        }

        public void SetCurrentReward(Wheel.WheelOfFortune.Reward reward)
        {
            _reward = reward;

            FillSlot(_reward);
        }

        public Wheel.WheelOfFortune.Reward GetCurrentReward()
        {
            return _reward;
        }
    }
}