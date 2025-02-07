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

        public ShopController(ItemDatabaseScriptableObject shopInitialData, EventService eventService, ItemService itemService)
        {
            this._eventService = eventService;
            this._shopInitialData = shopInitialData;
            this._itemService = itemService;

            _shopModel = new ShopModel();
            _shopModel.SetController(this);

            itemControllersList = new List<ItemController>();

            _eventService.OnFilterItemEvent.AddListener(OnFilterButtonChange);
        }

        ~ShopController() 
        {
            _eventService.OnFilterItemEvent.RemoveListener(OnFilterButtonChange);
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
            UpdateShopUI(type);
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
    }
}