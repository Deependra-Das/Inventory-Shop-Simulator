using System.Collections.Generic;
using ServiceLocator.Item;

namespace ServiceLocator.Inventory
{
    public class InventoryModel
    {
        private InventoryController _inventoryController;

        private List<ItemModel> inventoryItemList;

        public InventoryModel()
        {
            inventoryItemList = new List<ItemModel>();
        }

        ~InventoryModel() { }

        public void SetController(InventoryController inventoryController)
        {
            _inventoryController = inventoryController;
        }

        public void AddItem(ItemModel newItem)
        {
            inventoryItemList.Add(newItem);
        }

        public void RemoveItem(ItemModel item)
        {
            inventoryItemList.Remove(item);
        }

    }
}

