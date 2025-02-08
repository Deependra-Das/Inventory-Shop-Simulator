using ServiceLocator.Item;
using UnityEngine;
using System.Collections.Generic;
using ServiceLocator.Event;

namespace ServiceLocator.Shop
{
    public class ShopController
    {
        private ShopModel _shopModel;
        private ItemDatabaseScriptableObject _shopInitialData;
        private List<ItemController> itemControllersList;
        private EventService _eventService;
        private ItemService _itemService;
        private ItemType _itemTypeSelectedFilter;

        public ShopController(ItemDatabaseScriptableObject shopInitialData, EventService eventService, ItemService itemService)
        {
            this._eventService = eventService;
            this._shopInitialData = shopInitialData;
            this._itemService = itemService;

            _shopModel = new ShopModel();
            _shopModel.SetController(this);

            itemControllersList = new List<ItemController>();

            _itemTypeSelectedFilter = ItemType.All;

            _eventService.OnFilterItemEvent.AddListener(OnFilterButtonChange);
            _eventService.OnSellItemsShopEvent.AddListener(OnSellItemsShop);
        }

        ~ShopController() 
        {
            _eventService.OnFilterItemEvent.RemoveListener(OnFilterButtonChange);
            _eventService.OnSellItemsShopEvent.RemoveListener(OnSellItemsShop);
        }

        public void PopulateShopData()
        {
            foreach (var itemData in _shopInitialData.itemDataList)
            {
                AddNewItemInShop(itemData);
            }

            SetInitialQuantityShopData();
        }

        public void AddNewItemInShop(ItemScriptableObject itemData)
        {
            ItemController itemController = _itemService.CreateItem(itemData, UI.UIContentPanels.Shop);
            _shopModel.AddItem(itemController.GetItemModel);
            itemControllersList.Add(itemController);
        }

        public void SetInitialQuantityShopData()
        {
            foreach (ItemController item in itemControllersList)
            {
                item.UpdateQuantity(50);
            }

            UpdateShopUI(ItemType.All);
        }

        public List<ItemController> GetAllShopItems() => itemControllersList;


        private void OnFilterButtonChange(ItemType type)
        {
            _itemTypeSelectedFilter = type;
            UpdateShopUI(_itemTypeSelectedFilter);
        }

        private void UpdateShopUI(ItemType type)
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

        public int GetQuantityOfItem(ItemModel item)
        {
            int quantity = 0;
            foreach (ItemController itemCon in itemControllersList)
            { 
                if(itemCon.ItemName == item.ItemName)
                {
                    quantity = itemCon.Quantity;
                    break;
                }
            }
            return quantity;

        }

        public bool OnSellItemsShop(string itemName, int quantity)
        {
            bool itemUpdatedFlag = false;
            foreach (ItemController itemCon in itemControllersList)
            {
                if (itemCon.ItemName == itemName)
                {
                    itemCon.UpdateQuantity(itemCon.Quantity + quantity);
                    itemUpdatedFlag = true;
                    break;
                }
            }
            UpdateShopUI(_itemTypeSelectedFilter);
            return itemUpdatedFlag;
        }
    }
}