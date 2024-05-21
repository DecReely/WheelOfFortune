using System.Collections;
using System.Collections.Generic;
using CriticalStrike.WheelOfFortune.Misc;
using UnityEngine;

namespace CriticalStrike.WheelOfFortune.Item
{
    public class PlayerInventory : SingletonMonoBehaviour<PlayerInventory>
    {
        [SerializeField] private List<Wheel.WheelOfFortune.Reward> _inventory; // TODO: Remove serfield.

        protected override void Awake()
        {
            base.Awake();

            _inventory = new List<Wheel.WheelOfFortune.Reward>();
        }

        public List<Wheel.WheelOfFortune.Reward> GetInventory()
        {
            return _inventory;
        }

        public void AddToInventory(Wheel.WheelOfFortune.Reward reward)
        {
            int index = _inventory.FindIndex(x => x.Item.Type == reward.Item.Type);
            
            if (index == -1)
            {
                _inventory.Add(reward);
            }
            else
            {
                Wheel.WheelOfFortune.Reward modifiedReward = new Wheel.WheelOfFortune.Reward
                {
                    Item = reward.Item,
                    Quantity = _inventory[index].Quantity + reward.Quantity
                };

                _inventory[index] = modifiedReward;
            }
        }

        public bool DoesInventoryContainDeath()
        {
            int index = _inventory.FindIndex(x => x.Item.Type == RewardGenerator.Instance.GetDeathReward().Item.Type);

            return index != -1;
        }

        public void ResetInventory()
        {
            _inventory.Clear();
        }
    }
}

