using UnityEngine;
using System.Collections.Generic;
using ServiceLocator.Item;
using ServiceLocator.Event;

namespace ServiceLocator.Inventory
{
    public class InventoryController
    {
        private InventoryModel _inventoryModel;
        private ItemDatabaseScriptableObject _inventoryInitialData;
        private List<ItemController> itemControllersList;
        private EventService _eventService;
        private ItemService _itemService;

        public InventoryController(ItemDatabaseScriptableObject inventoryInitialData, EventService eventService, ItemService itemService)
        {
            this._eventService = eventService;
            this._inventoryInitialData = inventoryInitialData;
            this._itemService = itemService;

            _inventoryModel = new InventoryModel();
            _inventoryModel.SetController(this);

            itemControllersList = new List<ItemController>();

            _eventService.OnFilterItemEvent.AddListener(OnFilterButtonChange);
        }

        ~InventoryController()
        {
            _eventService.OnFilterItemEvent.RemoveListener(OnFilterButtonChange);
        }

        public void PopulateInventoryData()
        {
            foreach (var itemData in _inventoryInitialData.itemDataList)
            {
                AddNewItemInInventory(itemData);
            }
        }

        public void AddNewItemInInventory(ItemScriptableObject itemData)
        {
            ItemController itemController = _itemService.CreateItem(itemData, UI.UIContentPanels.Inventory);
            _inventoryModel.AddItem(itemController.GetItemModel);
            itemControllersList.Add(itemController);
        }

        public List<ItemController> GetAllInventoryItems() => itemControllersList;


        private void OnFilterButtonChange(ItemType type)
        {
            UpdateInventoryUI(type);
        }

        private void UpdateInventoryUI(ItemType type)
        {
            foreach (ItemController itemController in itemControllersList)
            {
                if (type == ItemType.All)
                {
                    if (itemController.Quantity > 0)
                    {
                        itemController.ShowItemButtonView();
                    }
                    else
                    {
                        itemController.HideItemButtonView();
                    }
                }
                else
                {
                    if (itemController.ItemType == type)
                    {
                        if (itemController.Quantity > 0)
                        {
                            itemController.ShowItemButtonView();
                        }
                        else
                        {
                            itemController.HideItemButtonView();
                        }
                    }
                    else
                    {
                        itemController.HideItemButtonView();
                    }

                }

            }
        }
    }
}