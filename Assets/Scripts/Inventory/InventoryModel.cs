using System.Collections.Generic;
using ServiceLocator.Item;

namespace ServiceLocator.Inventory
{
    public class InventoryModel
    {
        private InventoryController _inventoryController;

        private List<ItemModel> _inventoryItemList;

        private float maxWeight;
        private float currentWeight;
        private int numberOfItemsToSelect;

        public InventoryModel()
        {
            _inventoryItemList = new List<ItemModel>();
            maxWeight = 20.0f;
            numberOfItemsToSelect = 5;
            SetCurrentInventoryWeight();
        }

        ~InventoryModel() { }

        public void SetController(InventoryController inventoryController)
        {
            _inventoryController = inventoryController;
        }

        public void AddItem(ItemModel newItem)
        {
            _inventoryItemList.Add(newItem);
        }

        public void RemoveItem(ItemModel item)
        {
            _inventoryItemList.Remove(item);
        }

        public void SetCurrentInventoryWeight()
        {
            foreach (ItemModel item in _inventoryItemList)
            {
                currentWeight += (item.Weight * item.Quantity);
            }

        }

        public float CurrentInventoryWeight { get => currentWeight; }

        public float MaxInventoryWeight { get => maxWeight; }

        public int NumberOfItemsToSelect { get => numberOfItemsToSelect; }

        public List<ItemModel> InventoryItemList { get => _inventoryItemList; }
    }
}

