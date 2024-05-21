using System;
using System.Collections;
using System.Collections.Generic;
using CriticalStrike.WheelOfFortune.Core;
using CriticalStrike.WheelOfFortune.Misc;
using CriticalStrike.WheelOfFortune.Zone;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CriticalStrike.WheelOfFortune.Item
{
    [HelpURL("https://chatgpt.com/share/2631bfed-4f77-497d-aeb4-d498f5647f47")] // I know how to implement a weight based probability system but didn't want to code it from scratch.
    public class RewardGenerator : SingletonMonoBehaviour<RewardGenerator>
    {
        [Header("Database")]
        [SerializeField] private CardItemBase deathItem;
        [SerializeField] private List<CardItemBase> allItems;

        [Header("Settings")] 
        [Range(0, 50)] [SerializeField] private int randomnessPercentage;
        
        public Wheel.WheelOfFortune.Reward GenerateRandomReward()
        {
            CardItemBase item = SelectRandomItem(ZoneManager.Instance.GetCurrentZoneType());
            
            #region Generate Random Quantity

            int quantity = (int)item.RewardQuantityCurve.Evaluate(ZoneManager.Instance.GetCurrentZone());
            quantity = Random.Range(quantity * (100 - randomnessPercentage) / 100,
                quantity * (100 + randomnessPercentage) / 100);
            quantity = Math.Clamp(quantity, 1, int.MaxValue);

            #endregion
            
            Wheel.WheelOfFortune.Reward reward = new Wheel.WheelOfFortune.Reward
            {
                Item = item,
                Quantity = quantity
            };

            return reward;
        }
        
        public CardItemBase SelectRandomItem(Enums.ZoneType zoneType)
        {
            float totalWeight = 0;
            foreach (var card in allItems)
            {
                totalWeight += GetWeightByZone(card, zoneType);
            }

            float randomValue = Random.Range(0, totalWeight);
            float cumulativeWeight = 0;

            foreach (var card in allItems)
            {
                cumulativeWeight += GetWeightByZone(card, zoneType);
                if (randomValue <= cumulativeWeight)
                {
                    return card;
                }
            }

            // Fallback in case of rounding errors
            return allItems[allItems.Count - 1];
        }

        private float GetWeightByZone(CardItemBase card, Enums.ZoneType zoneType)
        {
            switch (zoneType)
            {
                case Enums.ZoneType.Normal:
                    return card.NormalZoneWeight;
                case Enums.ZoneType.Safe:
                    return card.SafeZoneWeight;
                case Enums.ZoneType.Super:
                    return card.SuperZoneWeight;
                default:
                    return 0;
            }
        }

        public Wheel.WheelOfFortune.Reward GetDeathReward()
        {
            return new Wheel.WheelOfFortune.Reward
            {
                Item = deathItem,
                Quantity = 0
            };
        }
    }
}

