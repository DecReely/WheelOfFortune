using System;
using System.Collections.Generic;
using CriticalStrike.WheelOfFortuneMiniGame.Core;
using CriticalStrike.WheelOfFortuneMiniGame.Misc;
using CriticalStrike.WheelOfFortuneMiniGame.Zone;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CriticalStrike.WheelOfFortuneMiniGame.Item
{
    [HelpURL("https://chatgpt.com/share/2631bfed-4f77-497d-aeb4-d498f5647f47")] // I know how to implement a weight based probability system but didn't want to code it from scratch.
    public class RewardGenerator : SingletonMonoBehaviour<RewardGenerator>
    {
        [Header("Database")]
        [SerializeField] private CardItemBase deathItem;
        [SerializeField] private List<CardItemBase> allItems;

        [Header("Settings")] 
        [Range(0, 50)] [SerializeField] private int randomnessPercentage = 30;
        
        public Reward GenerateRandomReward()
        {
            CardItemBase item = SelectRandomItem(ZoneManager.Instance.GetCurrentZoneType());
            
            #region Generate Random Quantity

            int quantity = (int)item.RewardQuantityCurve.Evaluate(ZoneManager.Instance.GetCurrentZone());
            quantity = Random.Range(quantity * (100 - randomnessPercentage) / 100,
                quantity * (100 + randomnessPercentage) / 100);
            quantity = Math.Clamp(quantity, 1, int.MaxValue);

            #endregion
            
            Reward reward = new Reward
            {
                Item = item,
                Quantity = quantity
            };

            return reward;
        }

        private CardItemBase SelectRandomItem(Enums.ZoneType zoneType)
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

        private static float GetWeightByZone(CardItemBase card, Enums.ZoneType zoneType)
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

        public Reward GetDeathReward()
        {
            return new Reward
            {
                Item = deathItem,
                Quantity = 0
            };
        }
    }
}

