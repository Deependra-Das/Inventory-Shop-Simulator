using ServiceLocator.Item;
using UnityEngine;
using System.Collections.Generic;

namespace ServiceLocator.Shop
{
    public class ShopController
    {
        private ShopScriptableObject _shopCurrentData;
        private List<ItemController> itemControllers;

        public ShopController(ShopScriptableObject shopCurrentData)
        {
            this._shopCurrentData = shopCurrentData;
           itemControllers = new List<ItemController>();
        }

        ~ShopController() 
        {
            this._shopCurrentData.shopItemList = new List<ItemWithQuantity>();
        }
        public void AddNewItemInShop(ItemWithQuantity itemData, ItemService itemService)
        {
            ItemController itemController = itemService.CreateItem(itemData, UI.UIContentPanels.Shop);
            _shopCurrentData.shopItemList.Add(itemController.ItemData);
            itemControllers.Add(itemController);
        }

        public List<ItemController> GetShopItems() => itemControllers;
    }
}