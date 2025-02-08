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
        private ItemType _itemTypeSelectedFilter;

        public InventoryController(ItemDatabaseScriptableObject inventoryInitialData, EventService eventService, ItemService itemService)
        {
            this._eventService = eventService;
            this._inventoryInitialData = inventoryInitialData;
            this._itemService = itemService;

            _inventoryModel = new InventoryModel();
            _inventoryModel.SetController(this);

            itemControllersList = new List<ItemController>();

            _itemTypeSelectedFilter = ItemType.All;

            _eventService.OnFilterItemEvent.AddListener(OnFilterButtonChange);
            _eventService.OnGatherResourcesEvent.AddListener(OnGatherResources);
            _eventService.OnSellItemsInventoryEvent.AddListener(OnSellItemsInventory);
        }

        ~InventoryController()
        {
            _eventService.OnFilterItemEvent.RemoveListener(OnFilterButtonChange);
            _eventService.OnGatherResourcesEvent.RemoveListener(OnGatherResources);
            _eventService.OnSellItemsInventoryEvent.RemoveListener(OnSellItemsInventory);
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
            _itemTypeSelectedFilter = type;
            UpdateInventoryUI(_itemTypeSelectedFilter);
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

            _eventService.OnInventoryWeightUpdateEvent.Invoke();
        }

        private void OnGatherResources()
        {
            List<int> randomItems = GetRandomItems();

            foreach (int item in randomItems)
            {
                itemControllersList[item].UpdateQuantity(itemControllersList[item].Quantity + GetRandomQuantity(itemControllersList[item].ItemType));
            }
            _inventoryModel.SetCurrentInventoryWeight();
            UpdateInventoryUI(_itemTypeSelectedFilter);
        }

        private List<int> GetRandomItems()
        {
            List<int> selectedItems = new List<int>(new int[_inventoryModel.InventoryItemList.Count]);

            int numberOfItemsToSelect = Random.Range(1, 4);  

            for (int i = 0; i < numberOfItemsToSelect; i++)
            {
                int randomIndex = Random.Range(0, _inventoryModel.InventoryItemList.Count);
                int selectedItem = randomIndex;

                if (selectedItems[randomIndex] != selectedItem)
                {
                    selectedItems.Insert(randomIndex, selectedItem);
                }
               
            }

            return selectedItems;
        }

        private int GetRandomQuantity(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Materials:
                    return Random.Range(5, 20);

                case ItemType.Weapons:
                    return Random.Range(0, 10);

                case ItemType.Consumables:
                    return Random.Range(5, 25);

                case ItemType.Treasures:
                    return Random.Range(1, 5);
                default:
                    return 0;
            }
        }

        public int GetQuantityOfItem(ItemModel item)
        {
            int quantity = 0;
            foreach (ItemController itemCon in itemControllersList)
            {
                if (itemCon.ItemName == item.ItemName)
                {
                    quantity = itemCon.Quantity;
                    break;
                }
            }
            return quantity;

        }

        public float GetCurrentInventoryWeight()
        {
            return _inventoryModel.CurrentInventoryWeight;
        }
        public float GetMaxInventoryWeight()
        {
            return _inventoryModel.MaxInventoryWeight;
        }

        public bool OnSellItemsInventory(string itemName,int quantity)
        {
            bool itemUpdatedFlag = false;
            foreach (ItemController itemCon in itemControllersList)
            {
                if (itemCon.ItemName == itemName)
                {
                    itemCon.UpdateQuantity(itemCon.Quantity - quantity);
                    itemUpdatedFlag = true;
                    break;
                }
            }
            UpdateInventoryUI(_itemTypeSelectedFilter);
            return itemUpdatedFlag;
        }
    }
}