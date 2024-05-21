using CriticalStrike.WheelOfFortuneMiniGame.Item;
using UnityEngine;

namespace CriticalStrike.WheelOfFortuneMiniGame.UI
{
    public class InventoryUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject inventorySlotPrefab;
        [SerializeField] private Transform inventorySlotParent;
    
        private void ClearInventoryUI()
        {
            foreach (Transform inventorySlot in inventorySlotParent)
            {
                Destroy(inventorySlot.gameObject);
            }
        }

        public void PrintInventoryUI()
        {
            ClearInventoryUI();
        
            for (int i = 0; i < PlayerInventory.Instance.GetInventory().Count; i++)
            {
                InventoryUISlot instantiatedUISlot = Instantiate(inventorySlotPrefab, inventorySlotParent).GetComponent<InventoryUISlot>();

                if (instantiatedUISlot != null)
                    instantiatedUISlot.SetCurrentReward(PlayerInventory.Instance.GetInventory()[i]);
            }
        }
    }
}