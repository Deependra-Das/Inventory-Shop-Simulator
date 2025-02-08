using System.Collections.Generic;
using ServiceLocator.Item;

namespace ServiceLocator.Inventory
{
    public class InventoryModel
    {
        private InventoryController _inventoryController;

        private List<ItemController> _inventoryItemList;

        private float maxWeight;
        private float currentWeight;
        private int numberOfItemsToSelect;

        public InventoryModel()
        {
            _inventoryItemList = new List<ItemController>();
            currentWeight = 0f;
            maxWeight = 100.0f;
            numberOfItemsToSelect = 5;
            SetCurrentInventoryWeight();
        }

        ~InventoryModel() { }

        public void SetController(InventoryController inventoryController)
        {
            _inventoryController = inventoryController;
        }

        public void AddItem(ItemController newItem)
        {
            _inventoryItemList.Add(newItem);
        }

        public void RemoveItem(ItemController item)
        {
            _inventoryItemList.Remove(item);
        }

        public void SetCurrentInventoryWeight()
        {
            currentWeight = 0f;
            foreach (ItemController item in _inventoryItemList)
            {
                currentWeight += (item.Weight * item.Quantity);
            }

        }

        public float CurrentInventoryWeight { get => currentWeight; }

        public float MaxInventoryWeight { get => maxWeight; }

        public int NumberOfItemsToSelect { get => numberOfItemsToSelect; }

        public List<ItemController> InventoryItemList { get => _inventoryItemList; }
    }
}

