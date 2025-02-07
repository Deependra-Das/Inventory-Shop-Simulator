using ServiceLocator.Item;
using UnityEngine;
using System.Collections.Generic;
using ServiceLocator.Event;

namespace ServiceLocator.Shop
{
    public class ShopController
    {
        private ShopScriptableObject _shopCurrentData;
        private List<ItemController> itemControllers;
        private EventService _eventService;

        public ShopController(ShopScriptableObject shopCurrentData, EventService eventService)
        {
            this._shopCurrentData = shopCurrentData;
            this._eventService = eventService;
           itemControllers = new List<ItemController>();
            _eventService.OnFilterItemEvent.AddListener(OnFilterButtonChange);
        }

        ~ShopController() 
        {
            this._shopCurrentData.shopItemList = new List<ItemWithQuantity>();
            _eventService.OnFilterItemEvent.RemoveListener(OnFilterButtonChange);
        }
        public void AddNewItemInShop(ItemWithQuantity itemData, ItemService itemService)
        {
            ItemController itemController = itemService.CreateItem(itemData, UI.UIContentPanels.Shop);
            _shopCurrentData.shopItemList.Add(itemController.ItemData);
            itemControllers.Add(itemController);
        }

        public List<ItemController> GetAllShopItems() => itemControllers;


        private void OnFilterButtonChange(ItemType type)
        {
            foreach (ItemController itemController in itemControllers)
            {
                if(type == ItemType.All)
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