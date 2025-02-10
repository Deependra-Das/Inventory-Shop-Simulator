using UnityEngine;
using System.Collections.Generic;
using ServiceLocator.Item;
using ServiceLocator.Event;
using ServiceLocator.Shop;
using System.Linq;

namespace ServiceLocator.Inventory
{
    public class InventoryController
    {
        private InventoryModel _inventoryModel;
        private List<ItemScriptableObject> _inventoryInitialDataOrdered;
        private EventService _eventService;
        private ItemService _itemService;
        private ItemType _itemTypeSelectedFilter;
        private ItemRarity _inventoryRarityValue;

        public InventoryController(ItemDatabaseScriptableObject inventoryInitialData, EventService eventService, ItemService itemService)
        {
            this._eventService = eventService;
            this._itemService = itemService;

            if (inventoryInitialData != null)
            {
                GetOrderedList(inventoryInitialData);

                _inventoryModel = new InventoryModel();
                _inventoryModel.SetController(this);

                _itemTypeSelectedFilter = ItemType.All;
                _inventoryRarityValue = ItemRarity.Common;
                SetEventListeners();
            }
            else
            {
                Debug.Log("Inventory Initial Data is Empty");
            }                
        }

        ~InventoryController()
        {
            RemoveEventListeners();        
        }

        private void SetEventListeners()
        {
            _eventService.OnFilterItemEvent.AddListener(OnFilterButtonChange);
            _eventService.OnGatherResourcesEvent.AddListener(OnGatherResources);
            _eventService.OnSellItemsInventoryEvent.AddListener(OnSellItemsInventory);
            _eventService.OnBuyItemsInventoryEvent.AddListener(OnBuyItemsInventory);
        }

        private void RemoveEventListeners()
        {
            _eventService.OnFilterItemEvent.RemoveListener(OnFilterButtonChange);
            _eventService.OnGatherResourcesEvent.RemoveListener(OnGatherResources);
            _eventService.OnSellItemsInventoryEvent.RemoveListener(OnSellItemsInventory);
            _eventService.OnBuyItemsInventoryEvent.RemoveListener(OnBuyItemsInventory);
        }

        public void PopulateInventoryData()
        {
            if (_inventoryInitialDataOrdered != null)
            {
                foreach (var itemData in _inventoryInitialDataOrdered)
                {
                    AddNewItemInInventory(itemData);
                }
            }
            else
            {
                Debug.Log("Inventory Ordered Data is Empty");
            }
        }

        public void AddNewItemInInventory(ItemScriptableObject itemData)
        {
            ItemController itemController = _itemService.CreateItem(itemData, UI.UIContentPanels.Inventory);
            _inventoryModel.AddItem(itemController);
        }

        private void OnFilterButtonChange(ItemType type)
        {
            _itemTypeSelectedFilter = type;
            UpdateInventoryUI(_itemTypeSelectedFilter);
        }

        private void UpdateInventoryUI(ItemType type)
        {
            foreach (ItemController itemController in _inventoryModel.InventoryItemList)
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
            List<ItemController> randomItems = GetRandomItems();

            foreach (ItemController item in randomItems)
            {
                int index = _inventoryModel.InventoryItemList.IndexOf(item);

                _inventoryModel.InventoryItemList[index].UpdateQuantity(_inventoryModel.InventoryItemList[index].Quantity + GetRandomQuantity(_inventoryModel.InventoryItemList[index].Rarity));
            }

            UpdateAfterInventoryAction();
            CheckWeightOvershoot();
        }

        private void CheckWeightOvershoot()
        {
            if (_inventoryModel.CurrentInventoryWeight >= _inventoryModel.MaxInventoryWeight)
            {
                _eventService.OnInventoryWeightOvershootEvent.Invoke();
            }
        }

        private List<ItemController> GetRandomItems()
        {
            List<ItemController> selectedItems = new List<ItemController>();

            int numberOfItemsToSelect = Random.Range(1, 4);  

            for (int i = 0; i < numberOfItemsToSelect; i++)
            {
                int randomIndex = Random.Range(0, _inventoryModel.InventoryItemList.Count);
                ItemController selectedItem = _inventoryModel.InventoryItemList[randomIndex];

                if (!selectedItems.Contains(selectedItem) && _inventoryModel.InventoryItemList[randomIndex].Rarity <= _inventoryRarityValue)
                {
                    selectedItems.Add(selectedItem);
                }
                else
                {
                    i--;
                    if (i < 0) { i = 0; }
                }
               
            }

            return selectedItems;
        }

        private int GetRandomQuantity(ItemRarity itemRarity)
        {
            switch (itemRarity)
            {
                case ItemRarity.VeryCommon:
                    return Random.Range(1, 20);

                case ItemRarity.Common:
                    return Random.Range(1, 15);

                case ItemRarity.Rare:
                    return Random.Range(1, 10);

                case ItemRarity.Epic:
                    return Random.Range(1, 5);

                case ItemRarity.Legendary:
                    return Random.Range(1, 2);
                default:
                    return 0;
            }
        }

        public int GetQuantityOfItem(ItemModel item)
        {
            int quantity = 0;
            foreach (ItemController itemCon in _inventoryModel.InventoryItemList)
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
            foreach (ItemController itemCon in _inventoryModel.InventoryItemList)
            {
                if (itemCon.ItemName == itemName)
                {
                    itemCon.UpdateQuantity(itemCon.Quantity - quantity);
                    itemUpdatedFlag = true;
                    break;
                }
            }

            UpdateAfterInventoryAction();

            return itemUpdatedFlag;
        }

        public bool OnBuyItemsInventory(string itemName, int quantity)
        {
            bool itemUpdatedFlag = false;
            foreach (ItemController itemCon in _inventoryModel.InventoryItemList)
            {
                if (itemCon.ItemName == itemName)
                {
                    itemCon.UpdateQuantity(itemCon.Quantity + quantity);
                    itemUpdatedFlag = true;
                    break;
                }
            }

            UpdateAfterInventoryAction();

            return itemUpdatedFlag;
        }

        private void UpdateAfterInventoryAction()
        {
            _inventoryModel.SetCurrentInventoryWeight();
            UpdateInventoryRarityValue();
            UpdateInventoryUI(_itemTypeSelectedFilter);
        }

        public List<ItemController> GetAllInventoryItems() => _inventoryModel.InventoryItemList;

        private void GetOrderedList(ItemDatabaseScriptableObject inventoryInitialData)
        {
            _inventoryInitialDataOrdered = inventoryInitialData.itemDataList
            .OrderBy(item => item.itemName)
            .GroupBy(item => item.itemName)
            .Select(group => group.First())
            .ToList();
        }

        private void UpdateInventoryRarityValue()
        {
            _inventoryRarityValue = ItemRarity.Common;
            foreach (ItemController itemCon in _inventoryModel.InventoryItemList)
            {
                if (itemCon.Rarity > _inventoryRarityValue && itemCon.Quantity >0)
                {
                    _inventoryRarityValue = itemCon.Rarity;
                }
            }
        }
    }
}