using CriticalStrike.WheelOfFortuneMiniGame.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CriticalStrike.WheelOfFortuneMiniGame.UI
{
    public class InventoryUISlot : MonoBehaviour
    {
        private Image _icon;
        private TextMeshProUGUI _quantityText;
        
        private Reward _reward;

        private void Awake()
        {
            _icon = GetComponentInChildren<Image>();
            _quantityText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void FillSlot(Reward reward)
        {
            _icon.sprite = reward.Item.Icon;
            _quantityText.SetText($"x{reward.Quantity}");
        }

        public void SetCurrentReward(Reward reward)
        {
            _reward = reward;

            FillSlot(_reward);
        }

        public Reward GetCurrentReward()
        {
            return _reward;
        }
    }
}