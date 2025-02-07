using UnityEngine;
using System.Collections.Generic;
using ServiceLocator.Item;
using ServiceLocator.Event;

namespace ServiceLocator.Inventory
{
    public class InventoryController
    {
        //private InventoryScriptableObject _inventoryCurrentData;
        //private List<ItemController> itemControllers;
        //private EventService _eventService;

        //public InventoryController(InventoryScriptableObject inventoryCurrentData, EventService eventService)
        //{
        //    this._inventoryCurrentData = inventoryCurrentData;
        //    this._eventService = eventService;
        //    itemControllers = new List<ItemController>();
        //    _eventService.OnFilterItemEvent.AddListener(OnFilterButtonChange);
        //}

        //~InventoryController()
        //{
        //    this._inventoryCurrentData.inventoryItemList = new List<ItemWithQuantity>();
        //    _eventService.OnFilterItemEvent.RemoveListener(OnFilterButtonChange);
        //}
        //public void AddNewItemInInventory(ItemWithQuantity itemData, ItemService itemService)
        //{
        //    ItemController itemController = itemService.CreateItem(itemData, UI.UIContentPanels.Inventory);
        //    _inventoryCurrentData.inventoryItemList.Add(itemController.ItemData);
        //    itemControllers.Add(itemController);
        //}

        //public List<ItemController> GetAllInventoryItems() => itemControllers;

        //private void OnFilterButtonChange(ItemType type)
        //{
        //    foreach (ItemController itemController in itemControllers)
        //    {
        //        if (type == ItemType.All)
        //        {
        //            if (itemController.Quantity > 0)
        //            {
        //                itemController.ShowItemButtonView();
        //            }
        //            else
        //            {
        //                itemController.HideItemButtonView();
        //            }
        //        }
        //        else
        //        {
        //            if (itemController.ItemType == type)
        //            {
        //                if (itemController.Quantity > 0)
        //                {
        //                    itemController.ShowItemButtonView();
        //                }
        //                else
        //                {
        //                    itemController.HideItemButtonView();
        //                }
        //            }
        //            else
        //            {
        //                itemController.HideItemButtonView();
        //            }

        //        }

        //    }

        //}
    }
}