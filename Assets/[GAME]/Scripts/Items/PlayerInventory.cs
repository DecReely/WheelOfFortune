using System.Collections.Generic;
using CriticalStrike.WheelOfFortuneMiniGame.Misc;

namespace CriticalStrike.WheelOfFortuneMiniGame.Item
{
    public class PlayerInventory : SingletonMonoBehaviour<PlayerInventory>
    {
        private List<Reward> _inventory;
        
        protected override void Awake()
        {
            base.Awake();

            _inventory = new List<Reward>();
        }

        public List<Reward> GetInventory()
        {
            return _inventory;
        }

        public void AddToInventory(Reward reward)
        {
            int index = _inventory.FindIndex(x => x.Item.Type == reward.Item.Type);
            
            if (index == -1)
            {
                _inventory.Add(reward);
            }
            else
            {
                Reward modifiedReward = new Reward
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

