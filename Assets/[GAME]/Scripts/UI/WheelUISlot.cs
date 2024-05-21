using System;
using CriticalStrike.WheelOfFortuneMiniGame.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CriticalStrike.WheelOfFortuneMiniGame.UI
{
    [ExecuteInEditMode]
    public class WheelUISlot : MonoBehaviour
    {
        private Image _icon;
        private TextMeshProUGUI _quantityText;
        
        [SerializeField] private Reward currentReward; // Left it serialized to make it changeable from editor.

        private void Awake()
        {
            _icon = GetComponentInChildren<Image>();
            _quantityText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnValidate()
        {
            ValidateReward();
            
            SetCurrentReward(currentReward);
        }

        private void ValidateReward()
        {
            if (currentReward.Quantity < 0)
            {
                int quantity = Math.Clamp(currentReward.Quantity, 0, int.MaxValue);
                
                currentReward.Quantity = quantity;
            }
        }

        private void FillSlot(Reward reward)
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

        public void SetCurrentReward(Reward reward)
        {
            currentReward = reward;

            FillSlot(currentReward);
        }

        public Reward GetCurrentReward()
        {
            return currentReward;
        }
    }
}

