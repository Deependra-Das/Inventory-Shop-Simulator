using UnityEngine;
using System.Collections.Generic;
using ServiceLocator.Item;

namespace ServiceLocator.Inventory
{
    public class InventoryController
    {
        private InventoryScriptableObject _inventoryCurrentData;
        private List<ItemController> itemControllers;

        public InventoryController(InventoryScriptableObject inventoryCurrentData)
        {
            this._inventoryCurrentData = inventoryCurrentData;
            itemControllers = new List<ItemController>();
        }

        ~InventoryController()
        {
            this._inventoryCurrentData.inventoryItemList = new List<ItemWithQuantity>();
        }
        public void AddNewItemInInventory(ItemWithQuantity itemData, ItemService itemService)
        {
            ItemController itemController = itemService.CreateItem(itemData, UI.UIContentPanels.Inventory);
            _inventoryCurrentData.inventoryItemList.Add(itemController.ItemData);
            itemControllers.Add(itemController);
        }

        public List<ItemController> GetInventoryItems() => itemControllers;
    }
}